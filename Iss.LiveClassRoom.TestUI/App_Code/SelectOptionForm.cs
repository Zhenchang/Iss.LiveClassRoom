using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Iss.LiveClassRoom.WebService.DataContracts.QuizData;

namespace Iss.LiveClassRoom.TestUI.App_Code
{
    public partial class SelectOptionForm : Form
    {
        GroupBox box = new GroupBox();
            
        public SelectOptionForm(string email, string password, string question, ICollection<OptionData> options)
        {
            InitializeComponent();
            textBoxEmail.Text = email;
            textBoxPwd.Text = password;
            labelQuestion.Text = question;
            box.AutoSize = true;
            int top = 0;
            foreach (var item in options)
            {
                RadioButton rd = new RadioButton();
                top += rd.Height;
                top += 2;
                rd.Top = top;
                rd.Text = item.Text;
                rd.Name = item.Id;
                box.Controls.Add(rd);
            }
            panelOption.AutoSize = true;
            panelOption.Controls.Add(box);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var client = Client.getClient(textBoxEmail.Text, textBoxPwd.Text);
            string optionId = "";
            foreach (var item in box.Controls)
            {
                if (item is RadioButton)
                {
                    if ((item as RadioButton).Checked)
                        optionId = (item as RadioButton).Name;
                }
            }
            client.AnswerQuiz(optionId);
        }
    }
}
