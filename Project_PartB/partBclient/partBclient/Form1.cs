using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace partBclient
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        public Form1()
        {
            InitializeComponent();
            client = new TcpClient();
            client.ConnectAsync("localhost", 1234);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string Username = username.Text;
            string Password = password.Text;

            // Create a message to send to the server.
            string message = Username+" "+Password;

            // Connect to the server (replace "localhost" and port with your server details).
            

            // Send the message to the server.
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
          
            await client.GetStream().WriteAsync(messageBytes, 0, messageBytes.Length);
            
            // Receive a response from the server.
            byte[] buffer = new byte[1024];
            int bytesRead = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            MessageBox.Show(response);
            // If the user is authenticated, show the message-sending form.
            if (response == "Authenticated")
            {
                var messageForm = new messageFormcs(client);
                this.Hide();
                messageForm.ShowDialog();
              //  this.Close();
            }
            else
            {
                // Display an error message.
                MessageBox.Show("Authentication failed. Please try again.");
            }
        }
    }
}
