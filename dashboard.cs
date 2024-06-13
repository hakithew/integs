using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
        }
        int timer = 1000;
        private void button1_Click(object sender, EventArgs e)
        {
            string to, from, pass, mail;
            to = textBox2.Text;
            from = "hakithewtv@gmail.com";
            string selectedDate = Date.Value.ToString("yyyy-MM-dd");
            string selectedTime = GetSelectedRadioButton();
            mail = $"Dear Customer,\n\nYour table reservation for {selectedDate} at {selectedTime} has been successfully received and confirmed. We look forward to serving you.\n\nBest Regards,\nGroup 1 Restaurant";
            pass = "ifzs asyw guvv dptj";
            MailMessage message = new MailMessage();
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = mail;
            message.Subject = "Table Reservation Confirmation";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);
            try
            {
                smtp.Send(message);
                MessageBox.Show("Reservation confirmation sent successfully! Check your email!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                button1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
            if (string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(textBox2.Text) ||
                string.IsNullOrEmpty(textBox3.Text) ||
                string.IsNullOrEmpty(textBox4.Text) ||
                !Date.Checked ||
                (!rb1.Checked && !rb2.Checked && !rb3.Checked && !rb4.Checked && !rb5.Checked && !rb6.Checked) ||
                comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlConnection con = new SqlConnection("Data Source=HKTHW\\SQLEXPRESS;Initial Catalog=dbRegister;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Reserve] ([fullname], [email], [phone], [numberofguest], [date], [time], [reservationtype]) " +
        "VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" +
        Date.Value.ToString("yyyy-MM-dd") + "', '" + GetSelectedRadioButton() + "', '" + comboBox1.SelectedItem.ToString() + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();  
            con.Close();
            MessageBox.Show("Reservation Submitted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private string GetSelectedRadioButton()
        {
            if (rb1.Checked) return rb1.Text;
            if (rb2.Checked) return rb2.Text;
            if (rb3.Checked) return rb3.Text;
            if (rb4.Checked) return rb4.Text;
            if (rb5.Checked) return rb5.Text;
            if (rb6.Checked) return rb6.Text;
            return string.Empty;

        }
    }
}

