using System;
using System.Windows.Forms;

namespace OneMore
{

  
    public partial class Form1 : Form
    {

        Mut mmut = new Mut();
        public Form1()
        {
            InitializeComponent();
            mmut.PDug += PDug;

        }

        private void PDug(string str)
        {

            //DLBox.AppendText(str + Environment.NewLine);

            DLBox.InvokeIfRequired(() => { DLBox.AppendText(str + Environment.NewLine); });



        }

        private void Form1_Load(object sender, EventArgs e)
        {


            mmut.Start(TGIP_box.Text, Convert.ToInt32(TGPORT_box.Text), Convert.ToInt32(Bind_box.Text));

        }



    }
    //擴充方法
    public static class Extension
    {
        //非同步委派更新UI
        public static void InvokeIfRequired(
            this Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)//在非當前執行緒內 使用委派
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
