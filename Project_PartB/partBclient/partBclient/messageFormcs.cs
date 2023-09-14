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
    public partial class messageFormcs : Form
    {
        private TcpClient client;
        public messageFormcs(TcpClient client)
        {
            InitializeComponent();
            this.client = client;
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
           
                try
                {
                    // Get the message from the textbox.
                    string message = messageTextBox.Text;

                    // Send the message to the server.
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    await client.GetStream().WriteAsync(messageBytes, 0, messageBytes.Length);

                    // Receive a response from the server.
                    byte[] buffer = new byte[1024];
                    int bytesRead = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // Display the response in a message box or a label on your form.
                    MessageBox.Show("Server Response: " + response);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }
    }
