using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic.FileIO;
using System.IO;


namespace caloriecheck
{
    public partial class Form1 : Form
    {
        private string f;

        public double sum1 { get; private set; }

        public Form1()
        {
            InitializeComponent();
            label6.Text = "";
            label25.Text = "";
            label26.Text = "";
            label27.Text = "";
            label19.Text = "";
            label20.Text = "";
            label21.Text = "";
            label22.Text = "";
            label28.Text = "";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = null;
            Microsoft.Office.Interop.Excel.Workbooks xlBooks = null;
            Microsoft.Office.Interop.Excel.Workbook xlBook = null;
            Microsoft.Office.Interop.Excel.Sheets xlSheets = null;
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = null;

            xlApp = new Microsoft.Office.Interop.Excel.Application();

            xlBooks = xlApp.Workbooks;

            xlBook = xlBooks.Open(System.IO.Path.GetFullPath(@"..\..\Book1.xlsm"));

            xlSheets = xlBook.Worksheets;
            xlSheet = xlSheets[1] as Microsoft.Office.Interop.Excel.Worksheet;

            xlApp.Visible = true;

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheets);

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks);

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
          

        }


        private void textToValue(string text, out int val)
        {
            if (int.TryParse(text, out val) == false)
                val = -1;
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void button2_Click(object sender, EventArgs e)
        {

            //身長//
            int bm;
            double bmi;
            textToValue(sintyou.Text, out bm);
            double bm1;
            bm1 = bm * 0.01;
            bmi = bm1 * bm1 * 22;
            //基礎代謝
            double kiso = 0;
            double kijun = 0;
            double old = 0;

            if (woman.Checked)
            {
                if (button18.Checked)
                {
                    kijun = 22.1;
                }
                else if (button30.Checked)
                {
                    kijun = 21.7;
                }
                else if (button50.Checked)
                {
                    kijun = 20.7;
                }
            }
            else
            {
                if (button18.Checked)
                {
                    kijun = 24.0;
                }
                else if (button30.Checked)
                {
                    kijun = 22.3;
                }
                else if (button50.Checked)
                {
                    kijun = 21.5;
                }
            }
            kiso = kijun * bmi;
            double lebel = csvread();
            double kcal = lebel * kiso;
            int kcal2 = (int)kcal;
            string kcal1 = kcal2.ToString();
            label20.Text = kcal1;

            string[] row = csvread2();
            label6.Text = row[0];
            label25.Text = row[1];
            label26.Text = row[2];
            label27.Text = row[3];
            int breakfast = int.Parse(row[0]);
            int lunch = int.Parse(row[1]);
            int dinner = int.Parse(row[2]);
            int other = int.Parse(row[3]);
            int sum = breakfast + lunch + dinner + other;
            label21.Text = sum.ToString();
            label22.Text = (sum - kcal2).ToString();
            if(kcal2 + 100 >= sum && kcal - 100 <= sum)
            {
                pictureBox1.ImageLocation = @"..\..\pict\1.png";
                label28.Text = "JUST!!健康的";
            }else if(sum > kcal2 + 100)
            {
                pictureBox1.ImageLocation = @"..\..\pict\2.png";
                label28.Text = "摂取カロリーを控えよう";
            }else if(sum < kcal2 - 100)
            {
                pictureBox1.ImageLocation = @"..\..\pict\3.png";
                label28.Text = "摂取カロリーを増やそう";
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

           
            


        }
         public double csvread()
        {
            TextFieldParser parser = new TextFieldParser(@"..\..\data.csv", System.Text.Encoding.GetEncoding("Shift_JIS"));
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

                string[] row = parser.ReadFields();
                double suimin1 = double.Parse(row[0]);
                double sum1 = (int)suimin1;
                suimin1 = suimin1 * 1.0;
                int sum = (int)suimin1;

                double sigoto1 = double.Parse(row[1]);
                sum1 = sum1 + (int)sigoto1;
                sigoto1 = sigoto1 * 1.6;
                sum = sum + (int)sigoto1;

                double sigoto2 = double.Parse(row[2]);
                sum1 = sum1 + (int)sigoto2;
                sigoto2 = sigoto2 * 2.2;
                sum = sum + (int)sigoto2;

                double sigoto3 = double.Parse(row[3]);
                sum1 = sum1 + (int)sigoto3;
                sigoto3 = sigoto3 * 4.5;
                sum = sum + (int)sigoto3;

                double bike = double.Parse(row[4]);
                sum1 = sum1 + (int)bike;
                bike = bike * 3.6;
                sum = sum + (int)bike;

                double train = double.Parse(row[5]);
                sum1 = sum1 + (int)train;
                train = train * 1.0;
                sum = sum + (int)train;

                double eating = double.Parse(row[6]);
                sum1 = sum1 + (int)eating;
                eating = eating * 1.4;
                sum = sum + (int)eating;

                double shopping = double.Parse(row[7]);
                sum1 = sum1 + (int)shopping;
                shopping = shopping * 2.2;
                sum = sum + (int)shopping;

                double shower = double.Parse(row[8]);
                sum1 = sum1 + (int)shower;
                shower = shower * 1.5;
                sum = sum + (int)shower;

                double reading = double.Parse(row[9]);
                sum1 = sum1 + (int)reading;
                reading = reading * 1.0;
                sum = sum + (int)reading;

                double homework = double.Parse(row[10]);
                sum1 = sum1 + (int)homework;
                homework = homework * 1.4;
                sum = sum + (int)homework;

                double ready = double.Parse(row[11]);
                sum1 = sum1 + (int)ready;
                ready = ready * 1.5;
                sum = sum + (int)ready;

                double wait = double.Parse(row[12]);
                sum1 = sum1 + (int)wait;
                wait = wait * 1.3;
                sum = sum + (int)wait;

                sum1 = sum / sum1;
                sum1 *= 100;
                sum1 = Math.Floor(sum1);
                sum1 /= 100;

                string s = sum1.ToString();
                label19.Text = s;

            return sum1;

           

          

        }
        public string[] csvread2()
        {
            TextFieldParser parser = new TextFieldParser(@"..\..\data2.csv", System.Text.Encoding.GetEncoding("Shift_JIS"));
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            string[] row2 = parser.ReadFields();

            return row2;


        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = null;
            Microsoft.Office.Interop.Excel.Workbooks xlBooks = null;
            Microsoft.Office.Interop.Excel.Workbook xlBook = null;
            Microsoft.Office.Interop.Excel.Sheets xlSheets = null;
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = null;

            xlApp = new Microsoft.Office.Interop.Excel.Application();

            xlBooks = xlApp.Workbooks;
            xlBook = xlBooks.Open(System.IO.Path.GetFullPath(@"..\..\calorie.xlsm"));

            xlSheets = xlBook.Worksheets;
            xlSheet = xlSheets[1] as Microsoft.Office.Interop.Excel.Worksheet;

            xlApp.Visible = true;

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheets);

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks);

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_KeyDown(object sender, KeyEventArgs e)
        {

        }


        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // appendをtrueにすると，既存のファイルに追記
                //         falseにすると，ファイルを新規作成する
                var append = true;
                // 出力用のファイルを開く
                using (var sw = new System.IO.StreamWriter(@"..\..\memory.csv", append))
                {
                    DateTime dtToday = DateTime.Today;
                    String today = dtToday.ToString();
                    sw.WriteLine(today + "," + label22.Text);
                }
            }
            catch (System.Exception en)
            {
                // ファイルを開くのに失敗したときエラーメッセージを表示
                System.Console.WriteLine(en.Message);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = null;
            Microsoft.Office.Interop.Excel.Workbooks xlBooks = null;
            Microsoft.Office.Interop.Excel.Workbook xlBook = null;
            Microsoft.Office.Interop.Excel.Sheets xlSheets = null;
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = null;

            xlApp = new Microsoft.Office.Interop.Excel.Application();

            xlBooks = xlApp.Workbooks;
            xlBook = xlBooks.Open(System.IO.Path.GetFullPath(@"..\..\memory.xlsm"));

            xlSheets = xlBook.Worksheets;
            xlSheet = xlSheets[1] as Microsoft.Office.Interop.Excel.Worksheet;

            xlApp.Visible = true;

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheets);

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks);

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
        }
    }



}

