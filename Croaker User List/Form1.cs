using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Croaker.CamfrogWin32;

namespace Croaker_User_List
{
    public partial class Form1 : Form
    {
        private CamfrogWin32 win32 = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            win32 = new CamfrogWin32();

            win32.RoomName = "PLaYa_L0uNgE";
            win32.GetChatRoomHandle();
            win32.GetTreeViewControl(win32.ChatRoomHandle);

            int result = win32.SendMessageToWindow(win32.UserList, CamfrogWin32.TVM_GETCOUNT).ToInt32();
            CamfrogWin32.SendMessage(win32.UserList, CamfrogWin32.TVM_SETIMAGELIST,
                (int)CamfrogWin32.TVSIL_STATE, imageList1.Handle.ToInt32());

            IntPtr root = CamfrogWin32.SendMessage(
                win32.UserList
                , CamfrogWin32.TVM_GETNEXTITEM
                , CamfrogWin32.TVGN_ROOT
                , 0);
            IntPtr nextItem = CamfrogWin32.SendMessage(
                root.ToInt32()
                , CamfrogWin32.TVM_GETNEXTITEM
                , CamfrogWin32.TVGN_NEXT
                , 0);


            //TVITEM tvi;

            //tvi.mask = CamfrogWin32.TVIF_HANDLE | CamfrogWin32.TVIF_STATE;
            //tvi.state = (uint)1;
            //tvi.state = tvi.state << 12;
            //tvi.stateMask = CamfrogWin32.TVIS_STATEIMAGEMASK;
            //tvi.hItem = root;
            //tvi.pszText = (IntPtr)0;
            //tvi.cchTextMax = 0;
            //tvi.iSelectedImage = 0;
            //tvi.cChildren = 0;
            //tvi.lParam = (IntPtr)0;

            //CamfrogWin32.SendMessage(
            //    new IntPtr(win32.UserList)
            //    , CamfrogWin32.TVM_SETITEM
            //    , 0
            //    , ref tvi);

        }

    }
}
