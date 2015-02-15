using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using mshtml;

namespace Croaker.CamfrogWin32
{
    public class CamfrogWin32
    {
        private int _chatRoomHandle = 0;
        private int _chatWindowHandle = 0;
        private int _chatTextArea = 0;
        private int _chatButton = 0;
        private int _userList = 0;

        private IHTMLDocument2 _htmlDocument = null;
        private int _whoOnMic = 0;
        private string _roomName;

        public delegate bool CallBackPtr(int hwnd, int lParam);
        public static Guid IID_IHTMLDocument2 = typeof(IHTMLDocument2).GUID;

        #region Constants
        public const int SMTO_ABORTIFHUNG = 0x2;
        public const int ENTER_CHAR = 0x0D;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_GETTEXT = 0x000D;
        public const int WM_GETTEXTLENGTH = 0x000E;
        public const int TVIF_TEXT = 0x0001;
        public const int TVIF_PARAM = 0x4;
        /* Tree view control */
        public const int TVM_CREATEDRAGIMAGE = 4370;
        public const int TVM_DELETEITEM = 4353;
        public const int TVM_ENDEDITLABELNOW = 4374;
        public const int TVM_ENSUREVISIBLE = 4372;
        public const int TVM_EXPAND = 4354;
        public const int TVM_GETCOUNT = 4357;
        public const int TVM_GETEDITCONTROL = 4367;
        public const int TVM_GETIMAGELIST = 4360;
        public const int TVM_GETINDENT = 4358;
        public const int TVM_GETITEMRECT = 4356;
        //public const int TVM_GETNEXTITEM = 4362;
        public const int TVM_GETVISIBLECOUNT = 4368;
        public const int TVM_HITTEST = 4369;
        public const int TVM_EDITLABELW = 4417;
        public const int TVM_GETISEARCHSTRINGW = 4416;
        public const int TVM_GETITEMW = 4414;
        public const int TVM_INSERTITEMW = 4402;
        public const int TVM_SETITEMW = 4415;
        public const int TVM_EDITLABELA = 4366;
        public const int TVM_GETISEARCHSTRINGA = 4375;
        public const int TVM_GETITEMA = 4364;
        public const int TVM_INSERTITEMA = 4352;
        public const int TVM_SETITEMA = 4365;
        //public const int TVM_SELECTITEM = 4363;
        public const int TVM_SETIMAGELIST = 4361;
        public const int TVM_SETINDENT = 4359;
        public const int TVM_SORTCHILDREN = 4371;
        public const int TVM_SORTCHILDRENCB = 4373;
        public const int TV_FIRST = 0x1100;
        public const int TVGN_ROOT = 0x0;
        public const int TVGN_NEXT = 0x1;
        public const int TVGN_CHILD = 0x4;
        public const int TVGN_FIRSTVISIBLE = 0x5;
        public const int TVGN_NEXTVISIBLE = 0x6;
        public const int TVGN_CARET = 0x9;
        public const int TVM_GETITEM = (TV_FIRST + 12);
        public const int TVM_SELECTITEM = (TV_FIRST + 11);
        public const int TVM_GETNEXTITEM = (TV_FIRST + 10);

        public const uint TVIF_HANDLE = 16;
        public const uint TVIF_STATE = 8;
        public const uint TVIS_STATEIMAGEMASK = 61440;
        public const uint TVM_SETITEM = TV_FIRST + 13;

        public const UInt32 TVSIL_NORMAL = 0;
        public const UInt32 TVSIL_STATE = 2;

        /* Tree view control notification */
        public const int TVN_KEYDOWN = -412;
        public const int TVN_BEGINDRAGW = -456;
        public const int TVN_BEGINLABELEDITW = -459;
        public const int TVN_BEGINRDRAGW = -457;
        public const int TVN_DELETEITEMW = -458;
        public const int TVN_ENDLABELEDITW = -460;
        public const int TVN_GETDISPINFOW = -452;
        public const int TVN_ITEMEXPANDEDW = -455;
        public const int TVN_ITEMEXPANDINGW = -454;
        public const int TVN_SELCHANGEDW = -451;
        public const int TVN_SELCHANGINGW = -450;
        public const int TVN_SETDISPINFOW = -453;
        public const int TVN_BEGINDRAGA = -407;
        public const int TVN_BEGINLABELEDITA = -410;
        public const int TVN_BEGINRDRAGA = -408;
        public const int TVN_DELETEITEMA = -409;
        public const int TVN_ENDLABELEDITA = -411;
        public const int TVN_GETDISPINFOA = -403;
        public const int TVN_ITEMEXPANDEDA = -406;
        public const int TVN_ITEMEXPANDINGA = -405;
        public const int TVN_SELCHANGEDA = -402;
        public const int TVN_SELCHANGINGA = -401;
        public const int TVN_SETDISPINFOA = -404;

        private const int BN_CLICKED = 0xF5;
        


#endregion

        #region Enums
        public enum GetWindow_Cmd : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6
        }
        #endregion

        #region Public Properties
        public int UserList
        {
            get { return _userList; }
            set { _userList = value; }
        }
        public int ChatButton
        {
            get { return _chatButton; }
            set { _chatButton = value; }
        }

        public int ChatTextArea
        {
            get { return _chatTextArea; }
            set { _chatTextArea = value; }
        }

        public IHTMLDocument2 HtmlDocument
        {
            get { return _htmlDocument; }
            set { _htmlDocument = value; }
        }

        public int ChatRoomHandle
        {
            get { return _chatRoomHandle; }
            set { _chatRoomHandle = value; }
        }

        public int ChatWindowHandle
        {
            get { return _chatWindowHandle; }
            set { _chatWindowHandle = value; }
        }

        public int TalkButton
        {
            get { return _whoOnMic; }
            set { _whoOnMic = value; }
        }
        public string RoomName
        {
            get { return _roomName; }
            set { _roomName = value; }
        }
        #endregion

        #region Public Methods
        public string GetWindowText(int hwnd)
        {
            int lmsg = RegisterWindowMessage("WM_GETTEXTLENGTH");
            int lRes = 0;
            SendMessageTimeout(new IntPtr(hwnd), lmsg, 0, 0, SMTO_ABORTIFHUNG, 10, out lRes);
            int length = SendMessage(hwnd, WM_GETTEXTLENGTH, 0, 0).ToInt32();

            StringBuilder sb = null;
            int len = GetWindowTextLength(hwnd) + 1;

            try
            {
                sb = new StringBuilder(len);
                GetWindowText(hwnd, sb, sb.Capacity);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sb != null ? sb.ToString() : null;
        }

        public int GetChatRoomHandle()
        {
            CallBackPtr callback = new CallBackPtr(ChatRoomCallback);
            EnumWindows(callback, 0);
            return _chatRoomHandle;
        }

        public bool ChatRoomCallback(int hWnd, int lParam)
        {
            if (GetWindowText(hWnd).ToLower().Contains(_roomName.ToLower()))
            {
                //List<IntPtr> childList = new List<IntPtr>();
                //GCHandle listHandle = GCHandle.Alloc(childList);
                Console.WriteLine(GetWindowText(hWnd));
                _chatRoomHandle = hWnd;
                return false;

                //try
                //{
                //    int nextHandle = GetWindow(hWnd, GetWindow_Cmd.GW_HWNDNEXT).ToInt32();

                //    do
                //    {
                //        if (GetWindowClass(nextHandle).Equals("#32770"))
                //            break;

                //        nextHandle = GetWindow(nextHandle, GetWindow_Cmd.GW_HWNDNEXT).ToInt32();
                        
                //    } while (nextHandle > 0);

                //    _chatRoomHandle = nextHandle;
                //    return false;
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
                //finally
                //{
                //    if (listHandle != null && listHandle.IsAllocated)
                //        listHandle.Free();
                //}
            }
            return true;
        }

        public int GetChatWindowHandle(int hWndParent)
        {
            int child1 = FindWindowByIndex(_chatRoomHandle, 4);
            int child2 = FindWindowByIndex(child1, 1);
            int child3 = FindWindowByIndex(child2, 1);
            int child4 = FindWindowByIndex(child3, 1);
            int child5 = FindWindowByIndex(child4, 6);
            int child6 = FindWindowByIndex(child5, 1);
            int child7 = FindWindowByIndex(child6, 1);
            int child8 = FindWindowByIndex(child7, 1);
            int child9 = FindWindowByIndex(child8, 1);
            _chatWindowHandle = child9;
            return child9;           
        }

        public int GetButton(int hWndParent)
        {
            int child1 = FindWindowByIndex(_chatRoomHandle, 4);
            int child2 = FindWindowByIndex(child1, 1);
            int child3 = FindWindowByIndex(child2, 6);
            _chatButton = child3;
            return child3;
        }

        public int GetTextArea(int hWndParent)
        {
            int child1 = FindWindowByIndex(_chatRoomHandle, 4);
            int child2 = FindWindowByIndex(child1, 1);
            int child3 = FindWindowByIndex(child2, 5);
            int child4 = FindWindowByIndex(child3, 1);
            int child5 = FindWindowByIndex(child4, 1);
            int child6 = FindWindowByIndex(child5, 1);
            int child7 = FindWindowByIndex(child6, 1);
            _chatTextArea = child7;            
            return child7;
        }

        public int GetWhoOnMicDialg(int hWndParent)
        {
            int child1 = FindWindowByIndex(hWndParent, 4);
            int child2 = FindWindowByIndex(child1, 1);
            int child3 = FindWindowByIndex(child2, 2);
            int child4 = FindWindowByIndex(child3, 12);
            int child5 = FindWindowByIndex(child4, 2);
            _whoOnMic = child5;
            return child5;

        }

        public IntPtr SendMessageToWindow(int hWnd, int cmd)
        {
            return SendMessage(hWnd, cmd, 0, 0);
        }

        public bool RefreshUpdateWindow(IntPtr hWnd)
        {
            return UpdateWindow(hWnd);
        }

        public void RegisterControlforMessages()
        {
            int gettextReg = RegisterWindowMessage("WM_GETTEXT");
            //int gettextlenReg = RegisterWindowMessage("WM_GETTEXTLENGTH");
        }

        public string GetChatText(int chatHandle)
        {
            IHTMLDocument2 doc = DocumentFromDOM(chatHandle);
            if (doc != null && (!string.IsNullOrEmpty(doc.body.innerText)))
                return doc.body.innerText;
            return null;
        }

        private bool ChatRoomChildCallback(int hwnd, int lParam)
        {
            StringBuilder sb = new StringBuilder(256);
            List<IntPtr> childList = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(childList);

            GetClassName(hwnd, sb, sb.Capacity);
            string className = sb.ToString();

            if (className.ToLower().Trim().Equals("internet explorer_server"))
            {
                IHTMLDocument2 doc = DocumentFromDOM(hwnd);

                if (doc != null && (!string.IsNullOrEmpty(doc.body.innerText) && doc.body.innerText.Length > 0) )
                {
                    _htmlDocument = doc;
                    _chatWindowHandle = hwnd;
                    return false;
                }
            }
            else
            {
                CallBackPtr callback = new CallBackPtr(ChatRoomChildCallback);
                EnumChildWindows(hwnd, callback, GCHandle.ToIntPtr(listHandle).ToInt32());
            }
            return true;
        }

        public int FindWindowByIndex(int hWndParent, int index)
        {
            IntPtr child = IntPtr.Zero;

            if (index == 0)
                return hWndParent;
            else
            {
                int i = 0;
                do
                {
                    child = FindWindowEx(hWndParent, child, null, null);
                    if (child != IntPtr.Zero)
                        ++i;
                } while (i < index && child != IntPtr.Zero);
            }

            return child.ToInt32();
        }

        public IHTMLDocument2 DocumentFromDOM(int hWnd)
        {
            int lenMsg = 0;
            int lRes = 0;
            IHTMLDocument2 document = null;

            lenMsg = RegisterWindowMessage("WM_HTML_GETOBJECT");
            if (lenMsg != 0)
            {
                SendMessageTimeout(new IntPtr(hWnd), lenMsg, 0, 0, SMTO_ABORTIFHUNG, 1000, out lRes);
                if (lRes > 0)
                {
                    int hr = ObjectFromLresult(lRes, ref IID_IHTMLDocument2, 0, ref document);
                }
            }

            return document;
        }

        public void SendText(string value, int delay)
        {
            string html = @"<HEAD><BASE href=""http://www.camfrog.com"">
            <STYLE type=text/css>
	html, body, p, div {
		border: 0;
		margin: 0;
		padding: 0;
	}
	html {
		height: 100%;
		overflow: hidden;
	}
	body {
		padding: 4px;
		word-wrap: break-word;
		background: transparent;
		height: 100%;
		overflow: auto;
	}
	p {
		letter-spacing: 0;
		line-height: 1.2;
	}
	.animated_smile {
		width: 24px;
		height: 24px;
		display: inline-block;
		overflow:hidden;
		zoom: 1;
    }
	.animated_smile img {
	}
</STYLE>

<SCRIPT type=text/javascript>
    var arItems = [];
    var checkVisibleFlag = true;
	var stop = true;
    function getOffsetRect(elem) {
        // (1)
        var box = elem.getBoundingClientRect()
        var body = document.body
        var docElem = document.documentElement
        // (2)
        var scrollTop = window.pageYOffset || docElem.scrollTop || body.scrollTop
        var scrollLeft = window.pageXOffset || docElem.scrollLeft || body.scrollLeft
        // (3)
        var clientTop = docElem.clientTop || body.clientTop || 0
        var clientLeft = docElem.clientLeft || body.clientLeft || 0
        // (4)
        var top  = box.top +  scrollTop - clientTop
        var left = box.left + scrollLeft - clientLeft
        return { top: Math.round(top), left: Math.round(left) }
    }
    function isVisible(elem) {
        var coords = getOffsetRect(elem);
        var windowTop = window.pageYOffset ||
            document.body.scrollTop;
        var windowBottom = windowTop +
            document.body.clientHeight;
        coords.bottom = coords.top + elem.offsetHeight;
        var topVisible = coords.top > windowTop
            && coords.top < windowBottom;
        var bottomVisible = coords.bottom < windowBottom
            && coords.bottom > windowTop;
       return topVisible || bottomVisible;
    }
    window.onresize = function() {
        checkVisibleFlag = true;
    }
    window.onscroll = function() {
        checkVisibleFlag = true;
    }
	function Scroll(){
		checkVisibleFlag = true;
    }
    function addToAnimation(divOb, imgWidth) {
        arItems.push({""divOb"": divOb, ""width"":imgWidth, ""visible"": true});
    }
    function generateId() {
        if (arItems.length)  {
            lastElementId = (arItems[arItems.length-1].divOb.id).replace(/smile-/, '');
            return 'smile-' + (lastElementId*1 + 1);
        } else {
            return 'smile-0';
        }
    }
    function Animation() {
		if (stop){
			var i;
			for( i in arItems) {
				if(arItems.length> i)
				{
					var mydiv = arItems[i].divOb;
					if (mydiv){
						try{
							mydiv.style.marginLeft = '0px';
						}catch(e){
							arItems.splice(i, 1);
							i--;
						}
					}
				}
			}
		}
		else{
			setTimeout(function(){
				var ii;
				for(ii in arItems) {
					if (arItems.length>ii)
					{
						var mydiv2 = arItems[ii].divOb;
						if (mydiv2) {
							try{
								var box = mydiv2.getBoundingClientRect();
								if (checkVisibleFlag) {
									arItems[ii].visible = isVisible( mydiv2);
								}
								if (arItems[ii].visible) {
									var pos_str = mydiv2.style.marginLeft;
									var pos = parseInt(pos_str.substr(0, pos_str.length - 2));
									pos -= mydiv2.height;
									if ((pos*(-1)) >=  arItems[ii].width) {
										pos = 0;
									}
									mydiv2.style.marginLeft = pos+'px';
								} else {
									mydiv2.style.marginLeft =0;
								}
							}catch(e){
								arItems.splice(ii, 1);
								ii--;
							}
						} else {
							arItems.splice(ii, 1);
							ii--;
						}
					}
				}
				checkVisibleFlag = false;
				Animation();
			}, 100);
		}
    }
    // start animation
    function startAnimation() {
		if(stop){
			//alert(""startAnimation_edit"");
			stop = false;
			Animation();
		}
    }
    // stop animation
    function stopAnimation() {
		if(!stop){
			//alert(""stopAnimation_edit"");
			stop = true;
			Animation();
		}
    }
    function  clearAllJsVar() {
        stopAnimation();
        arItems = [];
    }
	function  clearAllJsVar2() {
        arItems = [];
    }
    function addSmilesToAnimationFromBlock(block_id) {
        var block = document.getElementById(block_id); 
        var spans = block.getElementsByTagName(""span""); 
        for (var i = 0; i < spans.length; i++) { 
            var class_name = spans[i].className; 
            if (class_name == 'animated_smile') {
                addToAnimation(spans[i].childNodes[0], spans[i].childNodes[0].width);
            }
        }
    }
    window.onload = function() {
        //addSmilesToAnimationFromBlock('edit');
    }
</SCRIPT>
</HEAD>
<BODY style=""FONT-STYLE: normal; FONT-FAMILY: Calibri; COLOR: #000000; FONT-SIZE: 12pt; FONT-WEIGHT: bold; TEXT-DECORATION: none"" onscroll=Scroll() id=edit contentEditable=true>" + value + "</BODY>";
            IHTMLDocument2 doc = null;

            try
            {
                doc = this.DocumentFromDOM(_chatTextArea);
                Thread.Sleep(delay);
                doc.write(html);
                SendMessage(_chatButton,WM_LBUTTONDOWN, 0, 0);
                SendMessage(_chatButton, WM_LBUTTONUP, 0, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string GetWindowClass(int hWnd)
        {
            StringBuilder result = new StringBuilder(128);
            GetClassName(hWnd, result, result.Capacity);

            return result.ToString();
        }

        public string GetDialogItemText(int dlghWnd, int dlgTexthWnd)
        {
            StringBuilder result = new StringBuilder(256);

            GetDlgItemText(dlghWnd, dlgTexthWnd, result, result.Capacity);

            return result != null ? result.ToString() : null;
        }

        public bool GetIsWindowVisible(IntPtr hWnd)
        {
            return IsWindowVisible(hWnd);
        }

        public int GetTreeViewControl(int chatRoomHandle)
        {
            int child1 = FindWindowByIndex(chatRoomHandle, 4);
            int child2 = FindWindowByIndex(child1, 1);
            int child3 = FindWindowByIndex(child2, 1);
            int child4 = FindWindowByIndex(child3, 3);
            _userList = child4;
            string text = GetWindowClass(child4);
            return child3;
        }
        #endregion

        #region Dll Imports
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg,uint wParam, int lParam);

        [DllImport("user32.dll", EntryPoint="RegisterWindowMessageW", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        public static extern int RegisterWindowMessage(string lpString);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = System.Runtime.InteropServices.CharSet.Auto)] //
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(int hWnd, int Msg, int wparam, int lparam);

        [DllImport("User32", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg,uint wParam, ref TVITEM lParam);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(int hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(int hWnd, GetWindow_Cmd uCmd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetNextWindow(int hWnd, uint wCmd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetNextWindow(int hWnd, string wCmd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetWindowText(int hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int GetWindowTextLength(int hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(int hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        public static extern bool EnumWindows(CallBackPtr lpEnumFunc, int lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(int hwndParent, CallBackPtr lpEnumFunc, int lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(int hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "SendMessageTimeoutA")]
        public static extern int SendMessageTimeout(IntPtr hwnd, int msg, int wParam, int lParam, int fuFlags, int uTimeout, out int lpdwResult);

        [DllImport("OLEACC.dll")]
        public static extern int ObjectFromLresult(int lResult, ref Guid riid, int wParam, ref IHTMLDocument2 ppvObject);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetDlgItemText(int hDlg, int nIDDlgItem, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern bool UpdateWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);
        #endregion

        public CamfrogWin32()
        {
        }
    }

    #region Structs
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO
    {
        public uint cbSize;
        public RECT rcWindow;
        public RECT rcClient;
        public uint dwStyle;
        public uint dwExStyle;
        public uint dwWindowStatus;
        public uint cxWindowBorders;
        public uint cyWindowBorders;
        public ushort atomWindowType;
        public ushort wCreatorVersion;

        public WINDOWINFO(Boolean? filler)
            : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
        {
            cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
        }

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left, Top, Right, Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

        public int X
        {
            get { return Left; }
            set { Right -= (Left - value); Left = value; }
        }

        public int Y
        {
            get { return Top; }
            set { Bottom -= (Top - value); Top = value; }
        }

        public int Height
        {
            get { return Bottom - Top; }
            set { Bottom = value + Top; }
        }

        public int Width
        {
            get { return Right - Left; }
            set { Right = value + Left; }
        }

        public System.Drawing.Point Location
        {
            get { return new System.Drawing.Point(Left, Top); }
            set { X = value.X; Y = value.Y; }
        }

        public System.Drawing.Size Size
        {
            get { return new System.Drawing.Size(Width, Height); }
            set { Width = value.Width; Height = value.Height; }
        }

        public static implicit operator System.Drawing.Rectangle(RECT r)
        {
            return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
        }

        public static implicit operator RECT(System.Drawing.Rectangle r)
        {
            return new RECT(r);
        }

        public static bool operator ==(RECT r1, RECT r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(RECT r1, RECT r2)
        {
            return !r1.Equals(r2);
        }

        public bool Equals(RECT r)
        {
            return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
        }

        public override bool Equals(object obj)
        {
            if (obj is RECT)
                return Equals((RECT)obj);
            else if (obj is System.Drawing.Rectangle)
                return Equals(new RECT((System.Drawing.Rectangle)obj));
            return false;
        }

        public override int GetHashCode()
        {
            return ((System.Drawing.Rectangle)this).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct TVITEM
    {
        public uint mask;
        public IntPtr hItem;
        public uint state;
        public uint stateMask;
        //[MarshalAs(UnmanagedType.LPTStr)]
        public IntPtr pszText;
        public int cchTextMax;
        public int iImage;
        public int iSelectedImage;
        public int cChildren;
        public IntPtr lParam;
    }
    #endregion
}
