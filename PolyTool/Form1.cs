using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PolyTool
{
    public partial class Form1 : Form
    {
        const int WM_GETTEXTLENGTH = 0x000E;
        const int EM_SETSEL = 0x00B1;
        const int EM_REPLACESEL = 0x00C2;

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, int lParam);

        ListViewItemComparer listViewItemSorter;
        private int PerlFlag = 0, VGMFlag = 0, WorkerFlag = 0;
        private string Savepath, Temppath;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(@"C:\\Perl64\\bin\\perl.exe"))
            {
                PerlFlag = 1;
                WriteLog(String.Format(PolyTool.common.FoundActivePerl, "C:\\Perl64\\bin\\perl.exe"));
                if (File.Exists(Directory.GetCurrentDirectory() + "\\application\\test.exe"))
                {
                    VGMFlag = 1;
                }
                else
                {
                    VGMFlag = 0;
                }
            }
            else if (File.Exists(@"C:\\Strawberry\\perl\\bin\\perl.exe"))
            {
                PerlFlag = 1;
                WriteLog(String.Format(PolyTool.common.FoundStrawberryPerl, "C:\\Strawberry\\perl\\bin\\perl.exe"));
                if (File.Exists(Directory.GetCurrentDirectory() + "\\application\\test.exe"))
                {
                    VGMFlag = 1;
                }
                else
                {
                    VGMFlag = 0;
                }
            }
            else
            {
                PerlFlag = 0;
            }
            /*string[] program_list = GetUninstallList();
            foreach (string list in program_list)
            {
                if (list.Contains("ActivePerl"))
                {
                    PerlFlag = 1;
                }
                else if (list.Contains("Strawberry Perl"))
                {
                    PerlFlag = 1;
                }
            }*/
            if (PerlFlag != 1)
            {
                MessageBox.Show(this, PolyTool.common.PerlError, PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                if (VGMFlag != 1)
                {
                    MessageBox.Show(this, String.Format(PolyTool.common.VGMError, Directory.GetCurrentDirectory() + "\\application\\test.exe"), PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                else
                {
                    backgroundWorker_WH.DoWork += new DoWorkEventHandler(BackgroundWorker_WH_DoWork);
                    backgroundWorker_Siren14.DoWork += new DoWorkEventHandler(BackgroundWorker_Siren14_DoWork);
                    backgroundWorker_Siren14Enc.DoWork += new DoWorkEventHandler(BackgroundWorker_Siren14Enc_DoWork);
                    backgroundWorker_Move.DoWork += new DoWorkEventHandler(BackgroundWorker_Move_DoWork);

                    listView1.View = View.Details;
                    listView1.Columns.Add(PolyTool.common.ColumnsName, 150);
                    listView1.Columns.Add(PolyTool.common.ColumnsSize, 150);
                    listView1.Columns.Add(PolyTool.common.ColumnsFormat, 230);
                    listViewItemSorter = new ListViewItemComparer
                    {
                        ColumnModes =
                        new ListViewItemComparer.ComparerMode[]
                    {
                        ListViewItemComparer.ComparerMode.String,
                        ListViewItemComparer.ComparerMode.String,
                        ListViewItemComparer.ComparerMode.String,
                    }
                    };
                    listView1.ListViewItemSorter = listViewItemSorter;

                    WriteLog(PolyTool.common.RunningApp);
                }
            }
        }

        private void PolycomSiren14ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                FileName = "",
                InitialDirectory = "",
                Filter = PolyTool.common.FilterSiren14_NU,
                FilterIndex = 1,
                Title = PolyTool.common.ofdSiren14Title,
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var ini = new PrivateProfile.IniFile(@".\\settings.ini");
                FileInfo file = new FileInfo(ofd.FileName);
                long FileSize = file.Length;
                string sz = String.Format(PolyTool.common.Size, FileSize);
                string ext = Path.GetExtension(ofd.FileName);
                ListViewItem lvi;

                if (ext.ToUpper() == ".UNK")
                {
                    listView1.Items.Clear();
                    lvi = listView1.Items.Add(Path.GetFileNameWithoutExtension(ofd.FileName));
                    lvi.SubItems.Add(sz);
                    lvi.SubItems.Add(PolyTool.common.FormatUNK);
                }
                else if (ext.ToUpper() == ".S14")
                {
                    listView1.Items.Clear();
                    lvi = listView1.Items.Add(Path.GetFileNameWithoutExtension(ofd.FileName));
                    lvi.SubItems.Add(sz);
                    lvi.SubItems.Add(PolyTool.common.FormatS14);
                }
                else if (ext.ToUpper() == ".SSS")
                {
                    listView1.Items.Clear();
                    lvi = listView1.Items.Add(Path.GetFileNameWithoutExtension(ofd.FileName));
                    lvi.SubItems.Add(sz);
                    lvi.SubItems.Add(PolyTool.common.FormatSSS);
                }
                else if (ext.ToUpper() == ".NUB2")
                {
                    listView1.Items.Clear();
                    lvi = listView1.Items.Add(Path.GetFileNameWithoutExtension(ofd.FileName));
                    lvi.SubItems.Add(sz);
                    lvi.SubItems.Add(PolyTool.common.FormatNUB);
                }
                else if (ext.ToUpper() == ".NUS3BANK")
                {
                    listView1.Items.Clear();
                    lvi = listView1.Items.Add(Path.GetFileNameWithoutExtension(ofd.FileName));
                    lvi.SubItems.Add(sz);
                    lvi.SubItems.Add(PolyTool.common.FormatNUS);
                }
                else if (ext.ToUpper() == ".BNSF")
                {
                    listView1.Items.Clear();
                    lvi = listView1.Items.Add(Path.GetFileNameWithoutExtension(ofd.FileName));
                    lvi.SubItems.Add(sz);
                    lvi.SubItems.Add(PolyTool.common.FormatBNSF);
                }
                else
                {
                    listView1.Items.Clear();
                    button1.Enabled = false;
                    button2.Enabled = false;
                    MessageBox.Show(this, PolyTool.common.UnexpectedError, PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ini.WriteString("SETTINGS", "0x0000", "0");
                ini.WriteString("SETTINGS", "0x0001", ofd.FileName);
                button1.Enabled = true;
                button2.Enabled = false;
            }
        }

        private void WaveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                FileName = "",
                InitialDirectory = "",
                Filter = PolyTool.common.FilterWave,
                FilterIndex = 1,
                Title = PolyTool.common.ofdWaveTitle,
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var ini = new PrivateProfile.IniFile(@".\\settings.ini");
                FileInfo file = new FileInfo(ofd.FileName);
                long FileSize = file.Length;
                string sz = String.Format(PolyTool.common.Size, FileSize);
                string ext = Path.GetExtension(ofd.FileName);
                ListViewItem lvi;

                if (ext.ToUpper() == ".WAV")
                {
                    listView1.Items.Clear();
                    lvi = listView1.Items.Add(Path.GetFileNameWithoutExtension(ofd.FileName));
                    lvi.SubItems.Add(sz);
                    lvi.SubItems.Add(PolyTool.common.FormatWAVE);
                }
                else
                {
                    listView1.Items.Clear();
                    button1.Enabled = false;
                    button2.Enabled = false;
                    MessageBox.Show(this, PolyTool.common.UnexpectedError, PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ini.WriteString("SETTINGS", "0x0000", "0");
                ini.WriteString("SETTINGS", "0x0001", ofd.FileName);
                button1.Enabled = false;
                button2.Enabled = true;
            }
        }

        private void PolycomSiren14ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = PolyTool.common.fbdSiren14Title,
                RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = @"C:\\",
                ShowNewFolderButton = true
            };

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                var ini = new PrivateProfile.IniFile(@".\\settings.ini");
                listView1.Items.Clear();
                listView1.BeginUpdate();
                foreach (string file in Directory.GetFiles(fbd.SelectedPath + "\\", "*.*"))
                {
                    FileInfo fileinfo = new FileInfo(file);
                    long FileSize = fileinfo.Length;
                    string sz = String.Format(PolyTool.common.Size, FileSize);
                    ListViewItem lvi;
                    string ext = Path.GetExtension(file);
                    if (ext.ToUpper() == ".UNK")
                    {
                        lvi = listView1.Items.Add(Path.GetFileNameWithoutExtension(file));
                        lvi.SubItems.Add(sz);
                        lvi.SubItems.Add(PolyTool.common.FormatUNK);
                    }
                    else if (ext.ToUpper() == ".S14")
                    {
                        lvi = listView1.Items.Add(Path.GetFileNameWithoutExtension(file));
                        lvi.SubItems.Add(sz);
                        lvi.SubItems.Add(PolyTool.common.FormatS14);
                    }
                    else if (ext.ToUpper() == ".SSS")
                    {
                        lvi = listView1.Items.Add(Path.GetFileNameWithoutExtension(file));
                        lvi.SubItems.Add(sz);
                        lvi.SubItems.Add(PolyTool.common.FormatSSS);
                    }
                }
                listView1.EndUpdate();
                if (listView1.Items.Count < 1)
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    MessageBox.Show(this, PolyTool.common.UnexpectedErrorSiren14, PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ini.WriteString("SETTINGS", "0x0000", "1");
                ini.WriteString("SETTINGS", "0x0001", fbd.SelectedPath);
                button1.Enabled = true;
                button2.Enabled = false;
            }
        }

        private void WaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = PolyTool.common.fbdWaveTitle,
                RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = @"C:\\",
                ShowNewFolderButton = true
            };

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                var ini = new PrivateProfile.IniFile(@".\\settings.ini");
                listView1.Items.Clear();
                listView1.BeginUpdate();
                foreach (string file in Directory.GetFiles(fbd.SelectedPath + "\\", "*.*"))
                {
                    FileInfo fileinfo = new FileInfo(file);
                    long FileSize = fileinfo.Length;
                    string sz = String.Format(PolyTool.common.Size, FileSize);
                    ListViewItem lvi;
                    string ext = Path.GetExtension(file);
                    if (ext.ToUpper() == ".WAV")
                    {
                        lvi = listView1.Items.Add(Path.GetFileNameWithoutExtension(file));
                        lvi.SubItems.Add(sz);
                        lvi.SubItems.Add(PolyTool.common.FormatWAVE);
                    }
                }
                listView1.EndUpdate();
                if (listView1.Items.Count < 1)
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    MessageBox.Show(this, PolyTool.common.UnexpectedErrorWave, PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ini.WriteString("SETTINGS", "0x0000", "1");
                ini.WriteString("SETTINGS", "0x0001", fbd.SelectedPath);
                button1.Enabled = false;
                button2.Enabled = true;
            }
        }

        private void CloseCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            button1.Enabled = false;
            button2.Enabled = false;
            return;
        }

        private void ExitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            button1.Enabled = false;
            button2.Enabled = false;
            this.Close();
            return;
        }

        private void ConversionSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
            form.Dispose();
            return;
        }

        private void AboutAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.ShowDialog();
            form.Dispose();
            return;
        }

        private void CheckForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string netversion;
                WebClient wc = new WebClient();

                Stream st = wc.OpenRead("https://res.xyle-official.com/versions/polytool/VERSIONINFO");
                StreamReader sr = new StreamReader(st);
                netversion = sr.ReadToEnd();

                sr.Close();
                st.Close();

                FileVersionInfo ver = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);

                switch (ver.FileVersion.ToString().CompareTo(netversion.Substring(8)))
                {
                    case -1:
                        DialogResult dr = MessageBox.Show(this, PolyTool.common.Latest + netversion.Substring(8) + PolyTool.common.Current + ver.FileVersion + PolyTool.common.UpdateConfirm, PolyTool.common.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            Process.Start("https://github.com/xyle-gbp/polytool");
                            return;
                        }
                        else
                        {
                            return;
                        }
                    case 0:
                        MessageBox.Show(this, PolyTool.common.Latest + netversion.Substring(8) + PolyTool.common.Current + ver.FileVersion + PolyTool.common.Uptodate, PolyTool.common.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case 1:
                        throw new Exception(netversion.Substring(8).ToString() + " < " + ver.FileVersion.ToString());
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, PolyTool.common.UnexpectedError + "\r\n\r\n" + ex.ToString(), PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ListView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }

        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewItemSorter.Column = e.Column;
            listView1.Sort();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(Directory.GetCurrentDirectory() + "\\tmp");
                var ini = new PrivateProfile.IniFile(@".\\settings.ini");
                string path, Kbps1, Kbps2, KHz1, KHz2;
                UInt32 flag;
                path = ini.GetString("SETTINGS", "0x0001", null);
                Kbps1 = ini.GetString("SETTINGS", "0x1011", null);
                Kbps2 = ini.GetString("SETTINGS", "0x1031", null);
                KHz1 = ini.GetString("SETTINGS", "0x1021", null);
                KHz2 = ini.GetString("SETTINGS", "0x1041", null);
                flag = (UInt32)ini.GetInt("SETTINGS", "0x0000", 0xFFFF);
                ini.WriteString("SETTINGS", "0x1100", "1");

                if (flag != 0xFFFF)
                {
                    if (flag == 0)
                    {
                        if (Path.GetExtension(path).ToUpper() == ".NUB2" || Path.GetExtension(path).ToUpper() == ".NUS3BANK")
                        {
                            FolderBrowserDialog fbd = new FolderBrowserDialog
                            {
                                Description = PolyTool.common.fbdSaveTitle,
                                RootFolder = Environment.SpecialFolder.Desktop,
                                SelectedPath = @"C:\\",
                                ShowNewFolderButton = true
                            };
                            if (fbd.ShowDialog(this) == DialogResult.OK)
                            {
                                ClearLog();
                                if (Directory.GetFiles(fbd.SelectedPath, "*", SearchOption.AllDirectories).Count() != 0)
                                {
                                    DialogResult dr = MessageBox.Show(this, PolyTool.common.ExistWarning, PolyTool.common.WarningTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                    if (dr == DialogResult.Yes)
                                    {
                                        Delete(fbd.SelectedPath);
                                        Directory.CreateDirectory(fbd.SelectedPath);
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\tmp");

                                if (Path.GetExtension(path).ToUpper() == ".NUB2")
                                {
                                    Savepath = fbd.SelectedPath;
                                    this.Enabled = false;
                                    RefleshProgress_NU();
                                    backgroundWorker_NU_Single.RunWorkerAsync();

                                    while (this.backgroundWorker_NU_Single.IsBusy)
                                    {
                                        Application.DoEvents();
                                    }
                                    backgroundWorker_NU_Single.Dispose();
                                    WorkerFlag = 0;

                                    foreach (string file in Directory.GetFiles(Savepath + "\\", "*.wav"))
                                    {
                                        WriteLog(String.Format(PolyTool.common.Verifyed + "{0}'\r\n", file));
                                    }

                                    this.Enabled = true;
                                    MessageBox.Show(this, PolyTool.common.SuccessSiren14, PolyTool.common.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ResetProgress();
                                    listView1.Items.Clear();
                                    button1.Enabled = false;
                                    button2.Enabled = false;
                                    Process.Start(Savepath);
                                }
                                else if (Path.GetExtension(path).ToUpper() == ".NUS3BANK")
                                {
                                    Savepath = fbd.SelectedPath;
                                    this.Enabled = false;
                                    RefleshProgress_NU();
                                    backgroundWorker_NU_Single.RunWorkerAsync();

                                    while (this.backgroundWorker_NU_Single.IsBusy)
                                    {
                                        Application.DoEvents();
                                    }
                                    backgroundWorker_NU_Single.Dispose();
                                    WorkerFlag = 0;

                                    foreach (string file in Directory.GetFiles(Savepath + "\\", "*.wav"))
                                    {
                                        WriteLog(String.Format(PolyTool.common.Verifyed + "{0}'\r\n", file));
                                    }

                                    this.Enabled = true;
                                    MessageBox.Show(this, PolyTool.common.SuccessSiren14, PolyTool.common.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ResetProgress();
                                    listView1.Items.Clear();
                                    button1.Enabled = false;
                                    button2.Enabled = false;
                                    Process.Start(Savepath);
                                }
                                else
                                {
                                    button1.Enabled = false;
                                    button2.Enabled = false;
                                    MessageBox.Show(this, PolyTool.common.UnexpectedError, PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            SaveFileDialog sfd = new SaveFileDialog
                            {
                                FileName = Path.GetFileNameWithoutExtension(path),
                                InitialDirectory = "",
                                Filter = PolyTool.common.FilterWave,
                                FilterIndex = 1,
                                Title = PolyTool.common.sfdWaveTitle,
                                RestoreDirectory = true
                            };
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                ClearLog();
                                if (File.Exists(sfd.FileName))
                                {
                                    File.Delete(sfd.FileName);
                                }
                                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\tmp");
                                byte[] u2wFile = Properties.Resources.app_unk2wav;
                                byte[] whFile = Properties.Resources.app_waveheader;
                                byte[] plFile1 = Properties.Resources.sss2s14;
                                byte[] plFile2 = Properties.Resources.s14stereo;
                                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\siren2wav.exe", u2wFile);
                                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\waveheader.exe", whFile);

                                ProcessStartInfo pInfo = new ProcessStartInfo();
                                Process process;
                                if (Path.GetExtension(path).ToUpper() == ".UNK")
                                {
                                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\siren2wav.exe";
                                    pInfo.Arguments = "0 " + path + " " + sfd.FileName + " " + Kbps1 + " " + KHz1;
                                    pInfo.UseShellExecute = true;
                                    process = Process.Start(pInfo);
                                    process.WaitForExit();
                                }
                                else if (Path.GetExtension(path).ToUpper() == ".S14")
                                {
                                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\siren2wav.exe";
                                    pInfo.Arguments = "0 " + path + " " + sfd.FileName + " " + Kbps2 + " " + KHz2;
                                    pInfo.UseShellExecute = true;
                                    process = Process.Start(pInfo);
                                    process.WaitForExit();
                                }
                                else if (Path.GetExtension(path).ToUpper() == ".SSS")
                                {
                                    File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\sss2s14.plx", plFile1);
                                    File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\s14stereo.plx", plFile2);
                                    pInfo.FileName = "perl";
                                    pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\sss2s14.plx " + path;
                                    pInfo.UseShellExecute = true;
                                    process = Process.Start(pInfo);
                                    process.WaitForExit();

                                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\siren2wav.exe";
                                    pInfo.Arguments = "0 " + Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + "_L.S14 " + Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(sfd.FileName) + "_L.RAW " + Kbps2 + " " + KHz2;
                                    pInfo.UseShellExecute = true;
                                    process = Process.Start(pInfo);
                                    process.WaitForExit();

                                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\siren2wav.exe";
                                    pInfo.Arguments = "0 " + Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + "_R.S14 " + Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(sfd.FileName) + "_R.RAW " + Kbps2 + " " + KHz2;
                                    pInfo.UseShellExecute = true;
                                    process = Process.Start(pInfo);
                                    process.WaitForExit();
                                    File.Delete(Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + "_L.S14");
                                    File.Delete(Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + "_R.S14");

                                    pInfo.FileName = "perl";
                                    pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\s14stereo.plx " + Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(sfd.FileName) + "_L.RAW " + Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(sfd.FileName) + "_R.RAW " + Path.GetDirectoryName(sfd.FileName) + "\\" + Path.GetFileNameWithoutExtension(sfd.FileName) + ".RAW";
                                    pInfo.UseShellExecute = true;
                                    process = Process.Start(pInfo);
                                    process.WaitForExit();
                                    File.Delete(Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + "_L.RAW");
                                    File.Delete(Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + "_R.RAW");

                                    File.Move(Path.GetDirectoryName(sfd.FileName) + "\\" + Path.GetFileNameWithoutExtension(sfd.FileName) + ".RAW", Path.ChangeExtension(Path.GetDirectoryName(sfd.FileName) + "\\" + Path.GetFileNameWithoutExtension(sfd.FileName) + ".RAW", Path.GetExtension(sfd.FileName)));
                                }
                                else if (Path.GetExtension(path).ToUpper() == ".BNSF")
                                {
                                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\application\\test.exe";
                                    pInfo.Arguments = "-o " + sfd.FileName + " " + path;
                                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                    pInfo.UseShellExecute = true;
                                    process = Process.Start(pInfo);
                                    process.WaitForExit();
                                }
                                else
                                {
                                    button1.Enabled = false;
                                    button2.Enabled = false;
                                    MessageBox.Show(this, PolyTool.common.UnexpectedError, PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }


                                if (File.Exists(sfd.FileName))
                                {
                                    WriteLog(PolyTool.common.Verifyed + sfd.FileName + "\r\n");
                                    if (Path.GetExtension(path).ToUpper() == ".UNK")
                                    {
                                        pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\waveheader.exe";
                                        pInfo.Arguments = sfd.FileName + " 0";
                                        pInfo.UseShellExecute = true;
                                        process = Process.Start(pInfo);
                                        process.WaitForExit();
                                    }
                                    else if (Path.GetExtension(path).ToUpper() == ".S14")
                                    {
                                        pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\waveheader.exe";
                                        pInfo.Arguments = sfd.FileName + " 1";
                                        pInfo.UseShellExecute = true;
                                        process = Process.Start(pInfo);
                                        process.WaitForExit();
                                    }
                                    else if (Path.GetExtension(path).ToUpper() == ".SSS")
                                    {
                                        pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\waveheader.exe";
                                        pInfo.Arguments = sfd.FileName + " 2";
                                        pInfo.UseShellExecute = true;
                                        process = Process.Start(pInfo);
                                        process.WaitForExit();
                                    }
                                    else if (Path.GetExtension(path).ToUpper() == ".BNSF")
                                    {

                                    }
                                    else
                                    {
                                        button1.Enabled = false;
                                        button2.Enabled = false;
                                        MessageBox.Show(this, PolyTool.common.UnexpectedError, PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }

                                    if (File.Exists(Directory.GetCurrentDirectory() + "\\waveheader.bin"))
                                    {
                                        File.Delete(Directory.GetCurrentDirectory() + "\\waveheader.bin");
                                        File.Delete(sfd.FileName);
                                        WriteLog(string.Format(PolyTool.common.DecodedError, sfd.FileName) + "\r\n");
                                        Delete(Directory.GetCurrentDirectory() + "\\tmp");
                                        MessageBox.Show(this, PolyTool.common.ErrorSiren14, PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        listView1.Items.Clear();
                                        button1.Enabled = false;
                                        button2.Enabled = false;
                                        return;
                                    }
                                    else
                                    {
                                        WriteLog(string.Format(PolyTool.common.Decoded, sfd.FileName) + "\r\n");
                                        Delete(Directory.GetCurrentDirectory() + "\\tmp");
                                        MessageBox.Show(this, PolyTool.common.SuccessSiren14, PolyTool.common.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        listView1.Items.Clear();
                                        button1.Enabled = false;
                                        button2.Enabled = false;
                                        return;
                                    }
                                }
                                else
                                {
                                    WriteLog(PolyTool.common.NotVerifyed + sfd.FileName + "\r\n");
                                    Delete(Directory.GetCurrentDirectory() + "\\tmp");
                                    MessageBox.Show(this, PolyTool.common.ErrorSiren14, PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    listView1.Items.Clear();
                                    button1.Enabled = false;
                                    button2.Enabled = false;
                                    return;
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else if (flag == 1)
                    {
                        Delete(Directory.GetCurrentDirectory() + "\\tmp");
                        FolderBrowserDialog fbd = new FolderBrowserDialog
                        {
                            Description = PolyTool.common.fbdSaveTitle,
                            RootFolder = Environment.SpecialFolder.Desktop,
                            SelectedPath = @"C:\\",
                            ShowNewFolderButton = true
                        };
                        if (fbd.ShowDialog(this) == DialogResult.OK)
                        {
                            ClearLog();
                            if (Directory.GetFiles(fbd.SelectedPath, "*", SearchOption.AllDirectories).Count() != 0)
                            {
                                DialogResult dr = MessageBox.Show(this, PolyTool.common.ExistWarning, PolyTool.common.WarningTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dr == DialogResult.Yes)
                                {
                                    Delete(fbd.SelectedPath);
                                    Directory.CreateDirectory(fbd.SelectedPath);
                                }
                                else
                                {
                                    return;
                                }
                            }
                            button1.Enabled = false;
                            this.Enabled = false;
                            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\tmp");
                            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\tmp\\wkspace");
                            byte[] u2wFile = Properties.Resources.app_unk2wav;
                            byte[] whFile = Properties.Resources.app_waveheader;
                            byte[] plFile1 = Properties.Resources.sss2s14;
                            byte[] plFile2 = Properties.Resources.s14stereo;
                            File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\siren2wav.exe", u2wFile);
                            File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\waveheader.exe", whFile);
                            File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\sss2s14.plx", plFile1);
                            File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\s14stereo.plx", plFile2);

                            Savepath = fbd.SelectedPath;
                            RefleshProgress();
                            backgroundWorker_Siren14.RunWorkerAsync();

                            while (this.backgroundWorker_Siren14.IsBusy)
                            {
                                Application.DoEvents();
                            }
                            backgroundWorker_Siren14.Dispose();
                            WorkerFlag = 0;
                            WriteLog(PolyTool.common.RunningWH);
                            
                            backgroundWorker_WH.RunWorkerAsync();

                            while (this.backgroundWorker_WH.IsBusy)
                            {
                                Application.DoEvents();
                            }
                            backgroundWorker_WH.Dispose();
                            WorkerFlag = 0;

                            Delete(Directory.GetCurrentDirectory() + "\\tmp");
                            WriteLog(PolyTool.common.ProcessEnd);
                            MessageBox.Show(this, PolyTool.common.AllSiren14, PolyTool.common.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetProgress();
                            listView1.Items.Clear();
                            ClearLog();
                            this.Enabled = true;
                            button1.Enabled = false;
                            button2.Enabled = false;
                            Process.Start(Savepath);
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    throw new Exception(flag.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, PolyTool.common.UnexpectedError + "\r\n\r\n" + ex.ToString(), PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (Directory.Exists(Directory.GetCurrentDirectory() + "\\tmp"))
                {
                    Delete(Directory.GetCurrentDirectory() + "\\tmp");
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(Directory.GetCurrentDirectory() + "\\tmp");
                var ini = new PrivateProfile.IniFile(@".\\settings.ini");
                string path, Kbps1, Kbps2, KHz1, KHz2;
                UInt32 flag, radio;
                path = ini.GetString("SETTINGS", "0x0001", null);
                Kbps1 = ini.GetString("SETTINGS", "0x1011", null);
                Kbps2 = ini.GetString("SETTINGS", "0x1031", null);
                KHz1 = ini.GetString("SETTINGS", "0x1021", null);
                KHz2 = ini.GetString("SETTINGS", "0x1041", null);
                radio = (UInt32)ini.GetInt("SETTINGS", "0x1050", 0xFFFF);
                flag = (UInt32)ini.GetInt("SETTINGS", "0x0000", 0xFFFF);

                if (flag != 0xFFFF)
                {
                    if (flag == 0)
                    {
                        SaveFileDialog sfd = new SaveFileDialog
                        {
                            FileName = Path.GetFileNameWithoutExtension(path),
                            InitialDirectory = "",
                            Filter = PolyTool.common.FilterSiren14,
                            FilterIndex = 1,
                            Title = PolyTool.common.sfdSiren14Title,
                            RestoreDirectory = true
                        };
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            ClearLog();
                            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\tmp");
                            byte[] u2wFile = Properties.Resources.app_wav2unk;
                            File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\wav2siren.exe", u2wFile);

                            ProcessStartInfo pInfo = new ProcessStartInfo();
                            Process process;
                            if (Path.GetExtension(sfd.FileName).ToUpper() == ".UNK")
                            {
                                pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\wav2siren.exe";
                                pInfo.Arguments = "1 " + path + " " + sfd.FileName + " " + Kbps1 + " " + KHz1;
                                pInfo.UseShellExecute = true;
                                process = Process.Start(pInfo);
                                process.WaitForExit();
                            }
                            else if (Path.GetExtension(sfd.FileName).ToUpper() == ".S14")
                            {
                                pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\wav2siren.exe";
                                pInfo.Arguments = "1 " + path + " " + sfd.FileName + " " + Kbps2 + " " + KHz2;
                                pInfo.UseShellExecute = true;
                                process = Process.Start(pInfo);
                                process.WaitForExit();
                            }
                            else if (Path.GetExtension(sfd.FileName).ToUpper() == ".SSS")
                            {
                                pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\wav2siren.exe";
                                pInfo.Arguments = "1 " + path + " " + sfd.FileName + " " + Kbps2 + " " + KHz2;
                                pInfo.UseShellExecute = true;
                                process = Process.Start(pInfo);
                                process.WaitForExit();
                            }
                            else
                            {
                                button1.Enabled = false;
                                button2.Enabled = false;
                                MessageBox.Show(this, PolyTool.common.UnexpectedError, PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }


                            if (File.Exists(sfd.FileName))
                            {
                                WriteLog(PolyTool.common.Verifyed + sfd.FileName + "\r\n");
                                Delete(Directory.GetCurrentDirectory() + "\\tmp");
                                MessageBox.Show(this, PolyTool.common.SuccessWave, PolyTool.common.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                listView1.Items.Clear();
                                button1.Enabled = false;
                                button2.Enabled = false;
                                return;
                            }
                            else
                            {
                                WriteLog(PolyTool.common.NotVerifyed + sfd.FileName + "\r\n");
                                Delete(Directory.GetCurrentDirectory() + "\\tmp");
                                MessageBox.Show(this, PolyTool.common.ErrorWave, PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                listView1.Items.Clear();
                                button1.Enabled = false;
                                button2.Enabled = false;
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (flag == 1)
                    {
                        FolderBrowserDialog fbd = new FolderBrowserDialog
                        {
                            Description = PolyTool.common.fbdSaveTitle,
                            RootFolder = Environment.SpecialFolder.Desktop,
                            SelectedPath = @"C:\\",
                            ShowNewFolderButton = true
                        };
                        if (fbd.ShowDialog(this) == DialogResult.OK)
                        {
                            ClearLog();
                            if (Directory.GetFiles(fbd.SelectedPath, "*", SearchOption.AllDirectories).Count() != 0)
                            {
                                DialogResult dr = MessageBox.Show(this, PolyTool.common.ExistWarning, PolyTool.common.WarningTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dr == DialogResult.Yes)
                                {
                                    Delete(fbd.SelectedPath);
                                    Directory.CreateDirectory(fbd.SelectedPath);
                                }
                                else
                                {
                                    return;
                                }
                            }
                            button2.Enabled = false;
                            this.Enabled = false;
                            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\tmp");
                            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\tmp\\wkspace");

                            byte[] u2wFile = Properties.Resources.app_wav2unk;
                            File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\wav2siren.exe", u2wFile);
                            
                            Savepath = fbd.SelectedPath;
                            RefleshProgress();
                            backgroundWorker_Siren14Enc.RunWorkerAsync();

                            while (this.backgroundWorker_Siren14Enc.IsBusy)
                            {
                                Application.DoEvents();
                            }
                            backgroundWorker_Siren14Enc.Dispose();
                            WorkerFlag = 0;

                            backgroundWorker_Move.RunWorkerAsync();
                            while (this.backgroundWorker_Move.IsBusy)
                            {
                                Application.DoEvents();
                            }
                            backgroundWorker_Move.Dispose();
                            WorkerFlag = 0;

                            Delete(Directory.GetCurrentDirectory() + "\\tmp");
                            WriteLog(PolyTool.common.ProcessEnd);
                            MessageBox.Show(this, PolyTool.common.AllSiren14Enc, PolyTool.common.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetProgress();
                            listView1.Items.Clear();
                            ClearLog();
                            this.Enabled = true;
                            button1.Enabled = false;
                            button2.Enabled = false;
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    throw new Exception(flag.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, PolyTool.common.UnexpectedError + "\r\n\r\n" + ex.ToString(), PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (Directory.Exists(Directory.GetCurrentDirectory() + "\\tmp"))
                {
                    Delete(Directory.GetCurrentDirectory() + "\\tmp");
                }
            }
        }

        public static void Delete(string targetDirectoryPath)
        {
            if (!Directory.Exists(targetDirectoryPath))
            {
                return;
            }

            string[] filePaths = Directory.GetFiles(targetDirectoryPath);
            foreach (string filePath in filePaths)
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
            }

            string[] directoryPaths = Directory.GetDirectories(targetDirectoryPath);
            foreach (string directoryPath in directoryPaths)
            {
                Delete(directoryPath);
            }

            Directory.Delete(targetDirectoryPath, false);
        }

        public void RefleshProgress()
        {
            var ini = new PrivateProfile.IniFile(@".\\settings.ini");
            string path;
            path = ini.GetString("SETTINGS", "0x0001", null);
            if (path != null)
            {
                string[] files = System.IO.Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                int count = files.Count();

                progressBar1.Minimum = 0;
                progressBar1.Maximum = count * 2;
                progressBar1.Value = 0;
                label1.Text = PolyTool.common.Running;
            }
        }

        public void RefleshProgress_NU()
        {
            var ini = new PrivateProfile.IniFile(@".\\settings.ini");
            string path;
            path = ini.GetString("SETTINGS", "0x0001", null);
            int[] index = FindStreamIndex(path);
            byte[] ind = GetStreamIndex(index, path);
            if (ind != null)
            {
                int count = Convert.ToInt32(BitConverter.ToString(ind), 16);
                progressBar1.Minimum = 0;
                progressBar1.Maximum = count;
                progressBar1.Value = 0;
                label1.Text = PolyTool.common.Running;
            }
        }

        public void ResetProgress()
        {
            progressBar1.Value = 0;
            label1.Text = "";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Directory.Exists(Directory.GetCurrentDirectory() + "\\tmp"))
            {
                Delete(Directory.GetCurrentDirectory() + "\\tmp");
            }
        }

        private void BackgroundWorker_WH_DoWork(object sender, DoWorkEventArgs e)
        {
            if (WorkerFlag != 0)
            {
                return;
            }
            BackgroundWorker worker = (BackgroundWorker)sender;
            ProcessStartInfo pInfo = new ProcessStartInfo();
            Process process;
            Encoding enc = Encoding.GetEncoding("Shift_JIS");
            StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\decoded.log", false, enc);
            foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\tmp\\wkspace", "*.*"))
            {
                string fname = Path.GetFileNameWithoutExtension(file);
                if (Path.GetExtension(file).ToUpper() == ".WAV")
                {
                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\waveheader.exe";
                    pInfo.Arguments = file + " 0";
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                }
                else if (Path.GetExtension(file).ToUpper() == ".RAW")
                {
                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\waveheader.exe";
                    pInfo.Arguments = file + " 1";
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                }
                else if (Path.GetExtension(file).ToUpper() == ".SRAW")
                {
                    File.Move(file, Directory.GetCurrentDirectory() + "\\tmp\\" + fname + ".SRAW");
                    File.Move(Directory.GetCurrentDirectory() + "\\tmp\\" + fname + ".SRAW", Path.GetDirectoryName(file) + "\\" + fname + ".RAW");
                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\waveheader.exe";
                    pInfo.Arguments = Path.GetDirectoryName(file) + "\\" + fname + ".RAW" + " 2";
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                    File.Move(Path.GetDirectoryName(file) + "\\" + fname + ".RAW", Directory.GetCurrentDirectory() + "\\tmp\\" + fname + ".SRAW");
                    File.Move(Directory.GetCurrentDirectory() + "\\tmp\\" + fname + ".SRAW", Path.GetDirectoryName(file) + "\\" + fname + ".SRAW");
                }
                else
                {
                    WriteLog(PolyTool.common.UnexpectedError + "\r\n'" + file + "'\r\n" + PolyTool.common.UnexpectedErrorNotWave);
                }

                if (File.Exists(Directory.GetCurrentDirectory() + "\\waveheader.bin"))
                {
                    File.Delete(Directory.GetCurrentDirectory() + "\\waveheader.bin");
                    File.Delete(file);
                    writer.WriteLine(PolyTool.common.DecodedError, file);
                }
                else
                {
                    writer.WriteLine(PolyTool.common.Decoded, file);
                    File.Move(file, Savepath + "\\" + fname + ".wav");
                }
                Temppath = file;
                worker.ReportProgress(progressBar1.Maximum / 2 + Directory.GetFiles(Savepath, "*", SearchOption.AllDirectories).Count());
            }
            writer.Close();
            WorkerFlag = 1;
        }

        private void BackgroundWorker_WH_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int pp = progressBar1.Maximum / 2;
            int cr = pp - e.ProgressPercentage / 2;
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = PolyTool.common.Moved + cr.ToString();
            if (File.Exists(Directory.GetCurrentDirectory() + "\\waveheader.bin"))
            {
                WriteLog(PolyTool.common.File + Temppath + "' NG.\r\n");
            }
            else
            {
                WriteLog(PolyTool.common.File + Temppath + "' OK!\r\n");
            }
        }

        private void BackgroundWorker_Siren14_DoWork(object sender, DoWorkEventArgs e)
        {
            if (WorkerFlag != 0)
            {
                return;
            }
            var ini = new PrivateProfile.IniFile(@".\\settings.ini");
            string path, Kbps1, Kbps2, KHz1, KHz2;
            path = ini.GetString("SETTINGS", "0x0001", null);
            Kbps1 = ini.GetString("SETTINGS", "0x1011", null);
            Kbps2 = ini.GetString("SETTINGS", "0x1031", null);
            KHz1 = ini.GetString("SETTINGS", "0x1021", null);
            KHz2 = ini.GetString("SETTINGS", "0x1041", null);

            BackgroundWorker worker = (BackgroundWorker)sender;
            ProcessStartInfo pInfo = new ProcessStartInfo();
            Process process;

            string[] files = Directory.GetFiles(path + "\\", "*.*");
            foreach (string file in files)
            {
                string fname = Path.GetFileNameWithoutExtension(file);
                if (Path.GetExtension(file).ToUpper() == ".UNK")
                {
                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\siren2wav.exe";
                    pInfo.Arguments = "0 " + file + " " + Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + fname + ".wav " + Kbps1 + " " + KHz1;
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                }
                else if (Path.GetExtension(file).ToUpper() == ".S14")
                {
                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\siren2wav.exe";
                    pInfo.Arguments = "0 " + file + " " + Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + fname + ".raw " + Kbps2 + " " + KHz2;
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                }
                else if (Path.GetExtension(file).ToUpper() == ".SSS")
                {
                    File.Copy(file, Directory.GetCurrentDirectory() + "\\tmp\\" + fname + ".SSS");
                    pInfo.FileName = "perl";
                    pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\sss2s14.plx " + Directory.GetCurrentDirectory() + "\\tmp\\" + fname + ".SSS";
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();

                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\siren2wav.exe";
                    pInfo.Arguments = "0 " + Directory.GetCurrentDirectory() + "\\tmp\\" + fname + "_L.S14 " + Directory.GetCurrentDirectory() + "\\tmp\\" + fname + "_L.RAW " + Kbps2 + " " + KHz2;
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();

                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\siren2wav.exe";
                    pInfo.Arguments = "0 " + Directory.GetCurrentDirectory() + "\\tmp\\" + fname + "_R.S14 " + Directory.GetCurrentDirectory() + "\\tmp\\" + fname + "_R.RAW " + Kbps2 + " " + KHz2;
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                    File.Delete(Directory.GetCurrentDirectory() + "\\tmp\\" + fname + "_L.S14");
                    File.Delete(Directory.GetCurrentDirectory() + "\\tmp\\" + fname + "_R.S14");

                    pInfo.FileName = "perl";
                    pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\s14stereo.plx " + Directory.GetCurrentDirectory() + "\\tmp\\" + fname + "_L.RAW " + Directory.GetCurrentDirectory() + "\\tmp\\" + fname + "_R.RAW " + Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + fname + ".SRAW";
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                    File.Delete(Directory.GetCurrentDirectory() + "\\tmp\\" + fname + "_L.RAW");
                    File.Delete(Directory.GetCurrentDirectory() + "\\tmp\\" + fname + "_R.RAW");
                    File.Delete(Directory.GetCurrentDirectory() + "\\tmp\\" + fname + ".SSS");
                }
                else if (Path.GetExtension(file).ToUpper() == ".BNSF")
                {
                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\application\\test.exe";
                    pInfo.Arguments = "-o " + Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + fname + ".wav " + file;
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                }
                Temppath = file;
                worker.ReportProgress(Directory.GetFiles(Directory.GetCurrentDirectory() + "\\tmp\\wkspace", "*", SearchOption.AllDirectories).Count());
            }
            WorkerFlag = 1;
            return;
        }

        private void BackgroundWorker_Siren14Enc_DoWork(object sender, DoWorkEventArgs e)
        {
            if (WorkerFlag != 0)
            {
                return;
            }
            var ini = new PrivateProfile.IniFile(@".\\settings.ini");
            string path, Kbps1, Kbps2, KHz1, KHz2;
            UInt32 radio;
            path = ini.GetString("SETTINGS", "0x0001", null);
            Kbps1 = ini.GetString("SETTINGS", "0x1011", null);
            Kbps2 = ini.GetString("SETTINGS", "0x1031", null);
            KHz1 = ini.GetString("SETTINGS", "0x1021", null);
            KHz2 = ini.GetString("SETTINGS", "0x1041", null);
            radio = (UInt32)ini.GetInt("SETTINGS", "0x1050", 0xFFFF);

            BackgroundWorker worker = (BackgroundWorker)sender;
            ProcessStartInfo pInfo = new ProcessStartInfo();
            Process process;
            Encoding enc = Encoding.GetEncoding("Shift_JIS");
            StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\encoded.log", false, enc);

            foreach (string file in Directory.GetFiles(path + "\\", "*.*"))
            {
                string fname = Path.GetFileNameWithoutExtension(file);
                if (radio == 1)
                {
                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\wav2siren.exe";
                    pInfo.Arguments = "1 " + file + " " + Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + fname + ".unk " + Kbps1 + " " + KHz1;
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + fname + ".unk"))
                    {
                        writer.WriteLine(PolyTool.common.Encoded, file);
                    }
                    else
                    {
                        writer.WriteLine(PolyTool.common.EncodedError, file);
                    }
                }
                else if (radio == 2)
                {
                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\wav2siren.exe";
                    pInfo.Arguments = "1 " + file + " " + Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + fname + ".s14 " + Kbps2 + " " + KHz2;
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + fname + ".s14"))
                    {
                        writer.WriteLine(PolyTool.common.Encoded, file);
                    }
                    else
                    {
                        writer.WriteLine(PolyTool.common.EncodedError, file);
                    }
                }
                else if (radio == 3)
                {
                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\tmp\\wav2siren.exe";
                    pInfo.Arguments = "1 " + file + " " + Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + fname + ".sss " + Kbps2 + " " + KHz2;
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + fname + ".sss"))
                    {
                        writer.WriteLine(PolyTool.common.Encoded, file);
                    }
                    else
                    {
                        writer.WriteLine(PolyTool.common.EncodedError, file);
                    }
                }
                Temppath = file;
                worker.ReportProgress(Directory.GetFiles(Directory.GetCurrentDirectory() + "\\tmp\\wkspace", "*", SearchOption.AllDirectories).Count());
            }
            writer.Close();
            WorkerFlag = 1;
        }

        private void BackgroundWorker_Siren14Enc_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int pp = progressBar1.Maximum / 2;
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = PolyTool.common.Converted + e.ProgressPercentage.ToString() + " / " + pp.ToString();
            if (File.Exists(Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + Path.GetFileNameWithoutExtension(Temppath) + ".unk"))
            {
                WriteLog(PolyTool.common.Verifyed + Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + Path.GetFileNameWithoutExtension(Temppath) + ".unk'\r\n");
            }
            else
            {
                if (File.Exists(Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + Path.GetFileNameWithoutExtension(Temppath) + ".s14"))
                {
                    WriteLog(PolyTool.common.Verifyed + Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + Path.GetFileNameWithoutExtension(Temppath) + ".s14'\r\n");
                }
                else
                {
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + Path.GetFileNameWithoutExtension(Temppath) + ".sss"))
                    {
                        WriteLog(PolyTool.common.Verifyed + Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + Path.GetFileNameWithoutExtension(Temppath) + ".sss'\r\n");
                    }
                    else
                    {
                        WriteLog(String.Format(PolyTool.common.UnexpectedErrorNotWave, Temppath));
                    }
                }
            }
        }

        private void BackgroundWorker_Move_DoWork(object sender, DoWorkEventArgs e)
        {
            if (WorkerFlag != 0)
            {
                return;
            }
            BackgroundWorker worker = (BackgroundWorker)sender;
            foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\tmp\\wkspace", "*.*"))
            {
                string fname = Path.GetFileNameWithoutExtension(file);
                File.Move(file, Savepath + "\\" + fname + Path.GetExtension(file));

                Temppath = file;
                worker.ReportProgress(progressBar1.Maximum / 2 + Directory.GetFiles(Savepath, "*", SearchOption.AllDirectories).Count());
            }
            WorkerFlag = 1;
        }

        private void BackgroundWorker_Move_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int pp = progressBar1.Maximum / 2;
            int cr = pp - e.ProgressPercentage / 2;
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = PolyTool.common.Moved + cr.ToString();
        }

        private void BackgroundWorker_Siren14_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int pp = progressBar1.Maximum / 2;
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = PolyTool.common.Converted + e.ProgressPercentage.ToString() + " / " + pp.ToString();
            
            if (File.Exists(Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + Path.GetFileNameWithoutExtension(Temppath) + ".wav"))
            {  
                WriteLog(PolyTool.common.Verifyed + Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + Path.GetFileNameWithoutExtension(Temppath) + ".wav'\r\n");
            }
            else
            {
                if (File.Exists(Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + Path.GetFileNameWithoutExtension(Temppath) + ".raw"))
                {
                    WriteLog(PolyTool.common.Verifyed + Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + Path.GetFileNameWithoutExtension(Temppath) + ".raw'\r\n");
                }
                else
                {
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + Path.GetFileNameWithoutExtension(Temppath) + ".sraw"))
                    {
                        WriteLog(PolyTool.common.Verifyed + Directory.GetCurrentDirectory() + "\\tmp\\wkspace\\" + Path.GetFileNameWithoutExtension(Temppath) + ".sraw'\r\n");
                    }
                    else
                    {
                        WriteLog(String.Format(PolyTool.common.UnexpectedErrorNotSiren14, Temppath));
                    }
                }
            }
        }

        public void WriteLog(string logText)
        {
            SendMessage(textBox1.Handle, EM_REPLACESEL, 1, "[" + DateTime.Now.ToString() + "]->" + logText);
            //textBox1.AppendText("[" + DateTime.Now.ToString() + "]->" + logText);
        }

        public void ClearLog()
        {
            textBox1.Clear();
        }

        public class ListViewItemComparer : IComparer
        {
            /// <summary>
            /// 比較する方法
            /// </summary>
            public enum ComparerMode
            {
                /// <summary>
                /// 文字列として比較
                /// </summary>
                String,
                /// <summary>
                /// 数値（Int32型）として比較
                /// </summary>
                Integer,
                /// <summary>
                /// 日時（DataTime型）として比較
                /// </summary>
                DateTime,
                /// <summary>
                /// 比較しない
                /// </summary>
                None
            };

            private int _column;
            private SortOrder _order;
            private ComparerMode _mode;
            private ComparerMode[] _columnModes;

            /// <summary>
            /// 並び替えるListView列の番号
            /// </summary>
            public int Column
            {
                set
                {
                    //現在と同じ列の時は、昇順降順を切り替える
                    if (_column == value)
                    {
                        if (_order == SortOrder.Ascending)
                        {
                            _order = SortOrder.Descending;
                        }
                        else if (_order == SortOrder.Descending)
                        {
                            _order = SortOrder.Ascending;
                        }
                    }
                    _column = value;
                }
                get
                {
                    return _column;
                }
            }
            /// <summary>
            /// 昇順か降順か
            /// </summary>
            public SortOrder Order
            {
                set
                {
                    _order = value;
                }
                get
                {
                    return _order;
                }
            }
            /// <summary>
            /// 並び替えの方法
            /// </summary>
            public ComparerMode Mode
            {
                set
                {
                    _mode = value;
                }
                get
                {
                    return _mode;
                }
            }
            /// <summary>
            /// 列ごとの並び替えの方法
            /// </summary>
            public ComparerMode[] ColumnModes
            {
                set
                {
                    _columnModes = value;
                }
            }

            /// <summary>
            /// ListViewItemComparerクラスのコンストラクタ
            /// </summary>
            /// <param name="col">並び替える列の番号</param>
            /// <param name="ord">昇順か降順か</param>
            /// <param name="cmod">並び替えの方法</param>
            public ListViewItemComparer(
                int col, SortOrder ord, ComparerMode cmod)
            {
                _column = col;
                _order = ord;
                _mode = cmod;
            }
            public ListViewItemComparer()
            {
                _column = 0;
                _order = SortOrder.Ascending;
                _mode = ComparerMode.String;
            }

            //xがyより小さいときはマイナスの数、大きいときはプラスの数、
            //同じときは0を返す
            public int Compare(object x, object y)
            {
                if (_order == SortOrder.None)
                {
                    //並び替えない時
                    return 0;
                }

                int result = 0;
                //ListViewItemの取得
                ListViewItem itemx = (ListViewItem)x;
                ListViewItem itemy = (ListViewItem)y;

                //並べ替えの方法を決定
                if (_columnModes != null && _columnModes.Length > _column)
                {
                    _mode = _columnModes[_column];
                }

                //並び替えの方法別に、xとyを比較する
                try
                {
                    switch (_mode)
                    {
                        case ComparerMode.String:
                            //文字列をとして比較
                            result = string.Compare(itemx.SubItems[_column].Text,
                                itemy.SubItems[_column].Text);
                            break;
                        case ComparerMode.Integer:
                            //Int32に変換して比較
                            //.NET Framework 2.0からは、TryParseメソッドを使うこともできる
                            result = int.Parse(itemx.SubItems[_column].Text).CompareTo(
                                int.Parse(itemy.SubItems[_column].Text));
                            break;
                        case ComparerMode.DateTime:
                            //DateTimeに変換して比較
                            //.NET Framework 2.0からは、TryParseメソッドを使うこともできる
                            result = DateTime.Compare(
                                DateTime.Parse(itemx.SubItems[_column].Text),
                                DateTime.Parse(itemy.SubItems[_column].Text));
                            break;
                        case ComparerMode.None:
                            result = 0;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(null, PolyTool.common.UnexpectedError + "\r\n\r\n" + ex.ToString(), PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
                

                //降順の時は結果を+-逆にする
                if (_order == SortOrder.Descending)
                {
                    result = -result;
                }

                //結果を返す
                return result;
            }
        }

        static string[] GetUninstallList()
        {
            List<string> ret = new List<string>();
            string location = @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
            // レジストリの情報を取得する
            // ※取得できなかった場合は諦める
            Microsoft.Win32.RegistryKey parent = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(location, false);
            if (parent == null)
            {
                throw new Exception("Unknown Registry Key...");
            }
            // 子のレジストリの名前の数だけ処理をする
            foreach (string sub in parent.GetSubKeyNames())
            {
                // 子のレジストリの情報を取得する
                // ※取得できなかった場合は諦めて次のレジストリを処理する
                Microsoft.Win32.RegistryKey child = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(location + "\\" + sub, false);
                if (child == null)
                {
                    continue;
                }
                // 表示名を保持しているオブジェクトを取得する
                // ※取得できなかった場合は諦めて次のレジストリを処理する
                object value = child.GetValue("DisplayName");
                if (value == null)
                {
                    continue;
                }
                // 表示名をコンソールに表示する
                string name = value.ToString();
                ret.Add(name);
            }

            return ret.ToArray();
            /*List<string> ret = new List<string>();

            string uninstall_path = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
            string uninstall_path_x86 = "SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall";

            Microsoft.Win32.RegistryKey uninstall = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(uninstall_path, false);
            if (uninstall != null)
            {
                foreach (string subKey in uninstall.GetSubKeyNames())
                {
                    string appName = null;
                    Microsoft.Win32.RegistryKey appkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(uninstall_path + "\\" + subKey, false);

                    if (appkey.GetValue("DisplayName") != null)
                    {
                        appName = appkey.GetValue("DisplayName").ToString();
                    }
                    else
                    {
                        appName = subKey;
                    }

                    ret.Add(appName);
                }
            }
            else
            {
                throw new Exception("Unknown Registry Key.");
            }

            Microsoft.Win32.RegistryKey uninstall_x86 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(uninstall_path_x86, false);
            if (uninstall != null)
            {
                foreach (string subKey in uninstall_x86.GetSubKeyNames())
                {
                    string appName = null;
                    Microsoft.Win32.RegistryKey appkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(uninstall_path_x86 + "\\" + subKey, false);

                    if (appkey.GetValue("DisplayName") != null)
                    {
                        appName = appkey.GetValue("DisplayName").ToString();
                    }
                    else
                    {
                        appName = subKey;
                    }

                    ret.Add(appName);
                }
            }
            else
            {
                throw new Exception("Unknown Registry Key.");
            }

            return ret.ToArray();*/
        }

        private byte[] GetStreamIndex(int[] index, string fp)
        {
            List<byte> ret = new List<byte>();
            var fs = File.OpenRead(fp);
            try
            {
                BinaryReader br = new BinaryReader(fs);
                fs.Seek(index[1] + 8, SeekOrigin.Begin);
                ret.AddRange(br.ReadBytes(1));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, PolyTool.common.UnexpectedError + "\r\n\r\n" + ex.ToString(), PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fs.Close();
            }
            return ret.ToArray();
        }

        private void BackgroundWorker_NU_Single_DoWork(object sender, DoWorkEventArgs e)
        {
            if (WorkerFlag != 0)
            {
                return;
            }
            var ini = new PrivateProfile.IniFile(@".\\settings.ini");
            string path;
            path = ini.GetString("SETTINGS", "0x0001", null);
            BackgroundWorker worker = (BackgroundWorker)sender;
            ProcessStartInfo pInfo = new ProcessStartInfo();
            Process process;

            int[] index = FindStreamIndex(path);
            byte[] ind = GetStreamIndex(index, path);
            if (ind != null)
            {
                int count = Convert.ToInt32(BitConverter.ToString(ind), 16);
                
                for (int i = 0; i <= count; i++)
                {
                    if (count == i)
                    {
                        break;
                    }
                    pInfo.FileName = Directory.GetCurrentDirectory() + "\\application\\test.exe";
                    pInfo.Arguments = "-s " + i.ToString() + " -o " + Savepath + "\\" + "?s-?n.wav " + path;
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();

                    worker.ReportProgress(Directory.GetFiles(Savepath + "\\", "*", SearchOption.AllDirectories).Count());
                }
            }
            else
            {
                MessageBox.Show(this, PolyTool.common.UnexpectedError, PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            WorkerFlag = 1;
        }

        private void BackgroundWorker_NU_Single_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int pp = progressBar1.Maximum;
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = PolyTool.common.Converted + e.ProgressPercentage.ToString() + " / " + pp.ToString();

        }

        private void BackgroundWorker_NU_Multi_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void BackgroundWorker_NU_Multi_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private int[] FindStreamIndex(string fp)
        {
            List<int> ret = new List<int>();
            var fs = File.OpenRead(fp);
            try
            {
                var pattern = new byte[] { 84, 79, 78, 69 };
                byte[] data = new byte[0x2000];
                fs.Read(data, 0, 0x2000);

                List<int> positions = SearchBytePattern(pattern, data);

                foreach (var item in positions)
                {
                    ret.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, PolyTool.common.UnexpectedError + "\r\n\r\n" + ex.ToString(), PolyTool.common.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fs.Close();
            }
            return ret.ToArray();
        }

        static public List<int> SearchBytePattern(byte[] pattern, byte[] bytes)
        {
            List<int> positions = new List<int>();
            int patternLength = pattern.Length;
            int totalLength = bytes.Length;
            byte firstMatchByte = pattern[0];
            for (int i = 0; i < totalLength; i++)
            {
                if (firstMatchByte == bytes[i] && totalLength - i >= patternLength)
                {
                    byte[] match = new byte[patternLength];
                    Array.Copy(bytes, i, match, 0, patternLength);
                    if (match.SequenceEqual<byte>(pattern))
                    {
                        positions.Add(i);
                        i += patternLength - 1;
                    }
                }
            }
            return positions;
        }
    }
}
