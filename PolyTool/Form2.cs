using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolyTool
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var ini = new PrivateProfile.IniFile(@".\\settings.ini");
            UInt32 cmb1, cmb2, cmb3, cmb4, radio;
            cmb1 = (UInt32)ini.GetInt("SETTINGS", "0x1010", 0xFFFF);
            cmb2 = (UInt32)ini.GetInt("SETTINGS", "0x1020", 0xFFFF);
            cmb3 = (UInt32)ini.GetInt("SETTINGS", "0x1030", 0xFFFF);
            cmb4 = (UInt32)ini.GetInt("SETTINGS", "0x1040", 0xFFFF);
            radio = (UInt32)ini.GetInt("SETTINGS", "0x1050", 0xFFFF);

            if (cmb1 != 0xFFFF)
            {
                comboBox1.SelectedIndex = (int)cmb1;
            }
            else
            {
                comboBox1.SelectedIndex = 20;
                ini.WriteString("SETTINGS", "0x1010", comboBox1.SelectedIndex.ToString());
            }
            if (cmb2 != 0xFFFF)
            {
                comboBox2.SelectedIndex = (int)cmb2;
            }
            else
            {
                comboBox2.SelectedIndex = 1;
                ini.WriteString("SETTINGS", "0x1020", comboBox2.SelectedIndex.ToString());
            }
            if (cmb3 != 0xFFFF)
            {
                comboBox3.SelectedIndex = (int)cmb3;
            }
            else
            {
                comboBox3.SelectedIndex = 10;
                ini.WriteString("SETTINGS", "0x1030", comboBox3.SelectedIndex.ToString());
            }
            if (cmb4 != 0xFFFF)
            {
                comboBox4.SelectedIndex = (int)cmb4;
            }
            else
            {
                comboBox4.SelectedIndex = 1;
                ini.WriteString("SETTINGS", "0x1040", comboBox4.SelectedIndex.ToString());
            }
            if (radio != 0xFFFF)
            {
                if (radio == 1)
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                }
                else if (radio == 2)
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                    radioButton3.Checked = false;
                }
                else if (radio == 3)
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = true;
                }
                else
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                }
            }
            else
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                ini.WriteString("SETTINGS", "0x1050", "1");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var ini = new PrivateProfile.IniFile(@".\\settings.ini");

            ini.WriteString("SETTINGS", "0x1010", comboBox1.SelectedIndex.ToString());
            ini.WriteString("SETTINGS", "0x1020", comboBox2.SelectedIndex.ToString());
            ini.WriteString("SETTINGS", "0x1030", comboBox3.SelectedIndex.ToString());
            ini.WriteString("SETTINGS", "0x1040", comboBox4.SelectedIndex.ToString());
            switch (comboBox1.SelectedIndex)
            {
                case 0: // 16k
                    ini.WriteString("SETTINGS", "0x1011", "16000");
                    break;
                case 1: // 16.8k
                    ini.WriteString("SETTINGS", "0x1011", "16800");
                    break;
                case 2: // 17.6k
                    ini.WriteString("SETTINGS", "0x1011", "17600");
                    break;
                case 3: // 18.4k
                    ini.WriteString("SETTINGS", "0x1011", "18400");
                    break;
                case 4: // 19.2k
                    ini.WriteString("SETTINGS", "0x1011", "19200");
                    break;
                case 5: // 20k
                    ini.WriteString("SETTINGS", "0x1011", "20000");
                    break;
                case 6: // 20.8k
                    ini.WriteString("SETTINGS", "0x1011", "20800");
                    break;
                case 7: // 21.6k
                    ini.WriteString("SETTINGS", "0x1011", "21600");
                    break;
                case 8: // 22.4k
                    ini.WriteString("SETTINGS", "0x1011", "22400");
                    break;
                case 9: // 23.2k
                    ini.WriteString("SETTINGS", "0x1011", "23200");
                    break;
                case 10: // 24k
                    ini.WriteString("SETTINGS", "0x1011", "24000");
                    break;
                case 11: // 24.8k
                    ini.WriteString("SETTINGS", "0x1011", "24800");
                    break;
                case 12: // 25.6k
                    ini.WriteString("SETTINGS", "0x1011", "25600");
                    break;
                case 13: // 26.4k
                    ini.WriteString("SETTINGS", "0x1011", "26400");
                    break;
                case 14: // 27.2k
                    ini.WriteString("SETTINGS", "0x1011", "27200");
                    break;
                case 15: // 28k
                    ini.WriteString("SETTINGS", "0x1011", "28000");
                    break;
                case 16: // 28.8k
                    ini.WriteString("SETTINGS", "0x1011", "28800");
                    break;
                case 17: // 29.6k
                    ini.WriteString("SETTINGS", "0x1011", "29600");
                    break;
                case 18: // 30.4k
                    ini.WriteString("SETTINGS", "0x1011", "30400");
                    break;
                case 19: // 31.2k
                    ini.WriteString("SETTINGS", "0x1011", "31200");
                    break;
                case 20: // 32k
                    ini.WriteString("SETTINGS", "0x1011", "32000");
                    break;
                case 21: // 32.8k
                    ini.WriteString("SETTINGS", "0x1011", "32800");
                    break;
                case 22: // 33.6k
                    ini.WriteString("SETTINGS", "0x1011", "33600");
                    break;
                case 23: // 34.4k
                    ini.WriteString("SETTINGS", "0x1011", "34400");
                    break;
                case 24: // 35.2k
                    ini.WriteString("SETTINGS", "0x1011", "35200");
                    break;
                case 25: // 36k
                    ini.WriteString("SETTINGS", "0x1011", "36000");
                    break;
                case 26: // 36.8k
                    ini.WriteString("SETTINGS", "0x1011", "36800");
                    break;
                case 27: // 37.6k
                    ini.WriteString("SETTINGS", "0x1011", "37600");
                    break;
                case 28: // 38.4k
                    ini.WriteString("SETTINGS", "0x1011", "38400");
                    break;
                case 29: // 39.2k
                    ini.WriteString("SETTINGS", "0x1011", "39200");
                    break;
                case 30: // 40k
                    ini.WriteString("SETTINGS", "0x1011", "40000");
                    break;
                case 31: // 40.8k
                    ini.WriteString("SETTINGS", "0x1011", "40800");
                    break;
                case 32: // 41.6k
                    ini.WriteString("SETTINGS", "0x1011", "41600");
                    break;
                case 33: // 42.4k
                    ini.WriteString("SETTINGS", "0x1011", "42400");
                    break;
                case 34: // 43.2k
                    ini.WriteString("SETTINGS", "0x1011", "43200");
                    break;
                case 35: // 44k
                    ini.WriteString("SETTINGS", "0x1011", "44000");
                    break;
                case 36: // 44.8k
                    ini.WriteString("SETTINGS", "0x1011", "44800");
                    break;
                case 37: // 45.6k
                    ini.WriteString("SETTINGS", "0x1011", "45600");
                    break;
                case 38: // 46.4k
                    ini.WriteString("SETTINGS", "0x1011", "46400");
                    break;
                case 39: // 47.2k
                    ini.WriteString("SETTINGS", "0x1011", "47200");
                    break;
                case 40: //48k
                    ini.WriteString("SETTINGS", "0x1011", "48000");
                    break;
                default:
                    ini.WriteString("SETTINGS", "0x1011", null);
                    break;
            }
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    ini.WriteString("SETTINGS", "0x1021", "7000");
                    break;
                case 1:
                    ini.WriteString("SETTINGS", "0x1021", "14000");
                    break;
                default:
                    ini.WriteString("SETTINGS", "0x1021", null);
                    break;
            }
            switch (comboBox3.SelectedIndex)
            {
                case 0: // 16k
                    ini.WriteString("SETTINGS", "0x1031", "16000");
                    break;
                case 1: // 16.8k
                    ini.WriteString("SETTINGS", "0x1031", "16800");
                    break;
                case 2: // 17.6k
                    ini.WriteString("SETTINGS", "0x1031", "17600");
                    break;
                case 3: // 18.4k
                    ini.WriteString("SETTINGS", "0x1031", "18400");
                    break;
                case 4: // 19.2k
                    ini.WriteString("SETTINGS", "0x1031", "19200");
                    break;
                case 5: // 20k
                    ini.WriteString("SETTINGS", "0x1031", "20000");
                    break;
                case 6: // 20.8k
                    ini.WriteString("SETTINGS", "0x1031", "20800");
                    break;
                case 7: // 21.6k
                    ini.WriteString("SETTINGS", "0x1031", "21600");
                    break;
                case 8: // 22.4k
                    ini.WriteString("SETTINGS", "0x1031", "22400");
                    break;
                case 9: // 23.2k
                    ini.WriteString("SETTINGS", "0x1031", "23200");
                    break;
                case 10: // 24k
                    ini.WriteString("SETTINGS", "0x1031", "24000");
                    break;
                case 11: // 24.8k
                    ini.WriteString("SETTINGS", "0x1031", "24800");
                    break;
                case 12: // 25.6k
                    ini.WriteString("SETTINGS", "0x1031", "25600");
                    break;
                case 13: // 26.4k
                    ini.WriteString("SETTINGS", "0x1031", "26400");
                    break;
                case 14: // 27.2k
                    ini.WriteString("SETTINGS", "0x1031", "27200");
                    break;
                case 15: // 28k
                    ini.WriteString("SETTINGS", "0x1031", "28000");
                    break;
                case 16: // 28.8k
                    ini.WriteString("SETTINGS", "0x1031", "28800");
                    break;
                case 17: // 29.6k
                    ini.WriteString("SETTINGS", "0x1031", "29600");
                    break;
                case 18: // 30.4k
                    ini.WriteString("SETTINGS", "0x1031", "30400");
                    break;
                case 19: // 31.2k
                    ini.WriteString("SETTINGS", "0x1031", "31200");
                    break;
                case 20: // 32k
                    ini.WriteString("SETTINGS", "0x1031", "32000");
                    break;
                case 21: // 32.8k
                    ini.WriteString("SETTINGS", "0x1031", "32800");
                    break;
                case 22: // 33.6k
                    ini.WriteString("SETTINGS", "0x1031", "33600");
                    break;
                case 23: // 34.4k
                    ini.WriteString("SETTINGS", "0x1031", "34400");
                    break;
                case 24: // 35.2k
                    ini.WriteString("SETTINGS", "0x1031", "35200");
                    break;
                case 25: // 36k
                    ini.WriteString("SETTINGS", "0x1031", "36000");
                    break;
                case 26: // 36.8k
                    ini.WriteString("SETTINGS", "0x1031", "36800");
                    break;
                case 27: // 37.6k
                    ini.WriteString("SETTINGS", "0x1031", "37600");
                    break;
                case 28: // 38.4k
                    ini.WriteString("SETTINGS", "0x1031", "38400");
                    break;
                case 29: // 39.2k
                    ini.WriteString("SETTINGS", "0x1031", "39200");
                    break;
                case 30: // 40k
                    ini.WriteString("SETTINGS", "0x1031", "40000");
                    break;
                case 31: // 40.8k
                    ini.WriteString("SETTINGS", "0x1031", "40800");
                    break;
                case 32: // 41.6k
                    ini.WriteString("SETTINGS", "0x1031", "41600");
                    break;
                case 33: // 42.4k
                    ini.WriteString("SETTINGS", "0x1031", "42400");
                    break;
                case 34: // 43.2k
                    ini.WriteString("SETTINGS", "0x1031", "43200");
                    break;
                case 35: // 44k
                    ini.WriteString("SETTINGS", "0x1031", "44000");
                    break;
                case 36: // 44.8k
                    ini.WriteString("SETTINGS", "0x1031", "44800");
                    break;
                case 37: // 45.6k
                    ini.WriteString("SETTINGS", "0x1031", "45600");
                    break;
                case 38: // 46.4k
                    ini.WriteString("SETTINGS", "0x1031", "46400");
                    break;
                case 39: // 47.2k
                    ini.WriteString("SETTINGS", "0x1031", "47200");
                    break;
                case 40: //48k
                    ini.WriteString("SETTINGS", "0x1031", "48000");
                    break;
                default:
                    ini.WriteString("SETTINGS", "0x1031", null);
                    break;
            }
            switch (comboBox4.SelectedIndex)
            {
                case 0:
                    ini.WriteString("SETTINGS", "0x1041", "7000");
                    break;
                case 1:
                    ini.WriteString("SETTINGS", "0x1041", "14000");
                    break;
                default:
                    ini.WriteString("SETTINGS", "0x1041", null);
                    break;
            }
            switch (radioButton1.Checked)
            {
                case true:
                    ini.WriteString("SETTINGS", "0x1050", "1");
                    break;
                case false:
                    ini.WriteString("SETTINGS", "0x1050", null);
                    break;
                default:
                    ini.WriteString("SETTINGS", "0x1050", null);
                    break;
            }
            switch (radioButton2.Checked)
            {
                case true:
                    ini.WriteString("SETTINGS", "0x1050", "2");
                    break;
                case false:
                    ini.WriteString("SETTINGS", "0x1050", null);
                    break;
                default:
                    ini.WriteString("SETTINGS", "0x1050", null);
                    break;
            }
            switch (radioButton3.Checked)
            {
                case true:
                    ini.WriteString("SETTINGS", "0x1050", "3");
                    break;
                case false:
                    ini.WriteString("SETTINGS", "0x1050", null);
                    break;
                default:
                    ini.WriteString("SETTINGS", "0x1050", null);
                    break;
            }
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
