using Iss.LiveClassRoom.WebService.DataContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Iss.LiveClassRoom.TestUI.App_Code
{
    public partial class AnswerQuizForm : Form
    {
        ICollection<QuizData> quizList = new List<QuizData>();

        public AnswerQuizForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridView dgv = new DataGridView();
            panel2.Controls.Add(dgv);

            var client = Client.getClient(textBoxEmail.Text, textBoxPwd.Text);
            quizList = client.GetQuizListByCourseName(textBoxCourseName.Text);
            quizList = quizList.ToArray();
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("ID", typeof(string)));
            table.Columns.Add(new DataColumn("Question", typeof(string)));
            foreach (var item in quizList)
            {
                object[] objs = new object[2];
                objs[0] = item.Id;
                objs[1] = item.Question;
                table.Rows.Add(objs);
            }
            DataView dataView = table.DefaultView;
            dgv.DataSource = dataView;
            dgv.AutoSize = true;
            dgv.AutoResizeColumns();
            dgv.AutoResizeRows();
            dgv.CellContentClick += new DataGridViewCellEventHandler(dataGridViewQuiz_CellContentClick);
        }

        private void dataGridViewQuiz_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            new SelectOptionForm(textBoxEmail.Text, textBoxPwd.Text, (quizList.ToArray())[e.RowIndex].Question, (quizList.ToArray())[e.RowIndex].Options).ShowDialog();
        }

    }
}
