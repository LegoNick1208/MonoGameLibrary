using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Networking
{
    internal class ClientConnection : ConnectionManager
    {
        SteamCore Core;
        public override void OnMessage(nint data, int size, long messageNum, long recvTime, int channel)
        {
            if (Core == null) return;
            base.OnMessage(data, size, messageNum, recvTime, channel);
            byte[] bytes = new byte[size];
            Marshal.Copy(data, bytes, 0, size);
            Core.OnClientReceivedMessage(bytes);
        }
        public void Bind(SteamCore core)
        {
            this.Core = core;
        }
    }
}
