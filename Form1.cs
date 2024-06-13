using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
namespace LoginandRegistration
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }
        int timer = 1000;
        SqlConnection con = new SqlConnection("Data Source=HKTHW\\SQLEXPRESS;Initial Catalog=dbRegister;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "")
            {
                MessageBox.Show("Fill in the blanks", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox2.Text == textBox3.Text)
            {
                con.Open();
                string register = "INSERT INTO Registration VALUES('" + textBox1.Text + "', '" + textBox2.Text + "','" + textBox3.Text + "','"+textBox4.Text+"')";
                cmd = new SqlCommand(register, con);
                cmd.ExecuteNonQuery();
                con.Close();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";

                MessageBox.Show("Your account has been successfully created", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.None);


            }
            else
            {
                MessageBox.Show("Password does not match, Please Re-enter", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.None);
                textBox2.Text = "";
                textBox3.Text = "";
                textBox2.Focus();
            }

            
            
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '•';
                textBox3.PasswordChar = '•';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Focus();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Hide();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {

                timer1.Stop();
                string to, from, pass, mail;
                to = textBox4.Text;
                from = "hakithewtv@gmail.com";
                mail = timer.ToString();
                pass = "ifzs asyw guvv dptj";
                MailMessage message = new MailMessage();
                message.To.Add(to);
                message.From = new MailAddress(from);
                message.Body = mail;
                message.Subject = "Verification Code";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(from, pass);
                try
                {
                    smtp.Send(message);
                    MessageBox.Show("Verification code sent successfuly! Go to Gmail!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox5.Enabled = true;
                    button4.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox5.Text  == timer.ToString())
            {
                MessageBox.Show("Verified");
            }
            else
            {
                MessageBox.Show("Wrong Code");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer += 10;
            if(timer == 9999)
            {
                timer = 1000;
            }
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                e.Cancel = true;
                textBox4.Focus();
                errorProvider1.SetError(textBox4, "Please enter your email!");
            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(textBox4, null);
            }
        }
    }
}
