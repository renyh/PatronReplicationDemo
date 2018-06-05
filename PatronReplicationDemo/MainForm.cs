using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Text;
using System.Windows.Forms;

namespace PatronReplicationDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        IChannel m_serverChannel = null;
        void StartRemoteServer()
        {
            m_serverChannel = new IpcServerChannel("CardCenterChannel");
            RemotingConfiguration.ApplicationName = "CardCenterServer";

            //Register the server channel.
            ChannelServices.RegisterChannel(m_serverChannel, false);

            //Register this service type.
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(CardCenterServer),
                "CardCenterServer",
                WellKnownObjectMode.Singleton);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string strError = "";
            try
            {
                StartRemoteServer();
            }
            catch (Exception ex)
            {
                strError = "启动 Remoting 发生错误:" + ex.Message;
                MessageBox.Show(this, strError);
                return;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
