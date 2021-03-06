using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Text;
namespace GenocardApp
{
	public static class GeneApi {
		/// Выводит информацию об указанном гене
		public static string GetGeneInfo(string genename) {
			genename = genename.Trim();
			string currentString = null;
			StringBuilder builder = new StringBuilder();
			using (StreamReader sr = new StreamReader(@"C:\Users\fdshfgas\Documents\_USB_\Разработки 2020\App 2020\GenocardApp\Генокарта.txt", Encoding.Default)) {
				currentString = sr.ReadLine();
				
				while(currentString != "-Ген " + genename) {
					currentString = sr.ReadLine();
				}
				
				if (currentString.StartsWith("-Ген " + genename)) {
					while ((currentString != "-----")) {
						currentString = sr.ReadLine();
						if (currentString == "-----") break;
						builder.AppendLine(currentString);
					};
				}
			}
			return builder.ToString();
		}
		/// Возвращает краткое описание гена
		public static string GetShortDescription(string geneinfo) {
			StringBuilder builder = new StringBuilder();
			string[] parts = geneinfo.Split('\n');
			foreach (string part in parts) {
				if (part == "Описание гена и белка\r") {
					break;
				}
				builder.AppendLine(part);
			}
			return builder.ToString();
		}
		// Возвращает Описание гена и белка
		public static string GetDescriptionGeneAndProtein(string geneinfo) {
			StringBuilder builder = new StringBuilder();
			// считывать ли данные
			bool isread = false;
			string[] parts = geneinfo.Split('\n');
			
			foreach (string part in parts) {
				isread |= part == "Описание гена и белка\r";
				
				if (part == "Связанные болезни и признаки\r") {
					break;
				}
				
				if (isread) {
					builder.AppendLine(part);
				}
			}
			return builder.ToString();
		}
		// Возвращает Связанные болезни и признаки
		public static string GetCombinedIllnessAndCharacteristics(string geneinfo) {
			StringBuilder builder = new StringBuilder();
			// считывать ли данные
			bool isread = false;
			string[] parts = geneinfo.Split('\n');
			
			foreach (string part in parts) {
				isread |= part == "Связанные болезни и признаки\r";
				
				if (part == "Распределение мутаций в гене\r") {
					break;
				}
				
				if (isread) {
					builder.AppendLine(part);
				}
			}
			return builder.ToString();
		}
		// Возвращает Распределение мутаций в гене
		public static string GetDistributionInGen(string geneinfo) {
			StringBuilder builder = new StringBuilder();
			// считывать ли данные
			bool isread = false;
			string[] parts = geneinfo.Split('\n');
			
			foreach (string part in parts) {
				isread |= part == "Распределение мутаций в гене\r";
				
				if (part == "Перспективы генетической терапии\r") {
					break;
				}
				
				if (isread) {
					builder.AppendLine(part);
				}
			}
			return builder.ToString();
		}
		// Возвращает Перспективы генетической терапии
		public static string GetPerspectives(string geneinfo) {
			StringBuilder builder = new StringBuilder();
			string[] parts = geneinfo.Split('\n');
			bool isread = false;
			
			foreach (string part in parts) {
				isread |= part == "Перспективы генетической терапии\r";
				
				if (isread) {
					builder.AppendLine(part);
				}
			}
				
			return builder.ToString();
		}
	}
	
	public class GenocardForm : Form
	{
		public string ImagePath = @"C:\Users\fdshfgas\Documents\_USB_\Разработки 2020\App 2020\GenocardApp\Images\";
		public Panel panelHeader = new Panel();
		public Panel panelMenu = new Panel();
		public Button bGenocard = new Button();
		public PictureBox pictureLogo = new PictureBox();
		public Button bAboutProject = new Button();
		public Button bDictionary = new Button();
		public PictureBox pTitle = new PictureBox();
		public Button bExit = new Button();
		public PictureBox pClose = new PictureBox();
		public PictureBox pCollapse = new PictureBox();
		public PictureBox pExpand = new PictureBox();
		public Panel panelSeparator = new Panel();
		public Button bSearch = new Button();
		public TextBox tSearch = new TextBox();
		public Label lSearch = new Label();
		public Label lTitle = new Label();
		public Panel panelInfo = new Panel();
		public TextBox tGenocard = new TextBox();
		public PictureBox pCurrentItem = new PictureBox();
		public Panel panelContent = new Panel();
		public Label lAboutProject = new Label();
		public TextBox tDictTerms = new TextBox();
		
		public LinkLabel linkGenocard = new LinkLabel();
		public Label lCopyright = new Label();
		
		#region HANDLERS
		public string GetTextFromFile(string filepath) {
			string text = null;
			using(StreamReader sr = new StreamReader(filepath, Encoding.Default)) {
				text = sr.ReadToEnd();
			}
			return text;
		}
		
		public void CloseForm_Click(object sender, EventArgs args) {
			this.Close();
		}
		
		public void ExpandForm_Click(object sender, EventArgs args) {
			if (WindowState == FormWindowState.Normal) {
				WindowState = FormWindowState.Maximized;
			} else {
				WindowState = FormWindowState.Normal;
			}
		}
		
		public void CollapseForm_Click(object sender, EventArgs args) {
			if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized) {
				WindowState = FormWindowState.Minimized;
			}
		}
		
		int OffsetX, OffsetY;
		bool isMouseDown = false;
	    
	    private void MouseDown_Click(object sender, MouseEventArgs e)
	    {
	      if (e.Button == MouseButtons.Left)
	      {
	        this.isMouseDown = true;
	        Point screen = this.PointToScreen(new Point(e.X, e.Y));
	        this.OffsetX = this.Location.X - screen.X;
	        this.OffsetY = this.Location.Y - screen.Y;
	      }
	      else
	        this.isMouseDown = false;
	      if (e.Clicks != 2)
	        return;
	      this.isMouseDown = false;
	    }
	
	    private void MouseMove_Click(object sender, MouseEventArgs e)
	    {
	      if (!this.isMouseDown)
	        return;
	      if (this.WindowState == FormWindowState.Maximized)
	        this.WindowState = FormWindowState.Normal;
	      Point screen = this.panelHeader.PointToScreen(new Point(e.X, e.Y));
	      screen.Offset(new Point(OffsetX, OffsetY));
	      this.Location = screen;
	    }
	
	    private void MouseUp_Click(object sender, MouseEventArgs e)
	    {
	      this.isMouseDown = false;
	    }
	    
	    protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;
 
            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);
 
                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }
 
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- use 0x20000
                return cp;
            }
        }
		

        public void Genocard_Click(object sender, EventArgs args) {
        	pCurrentItem.Location = new Point(-1, 69);
        	this.lAboutProject.Visible = false;
        	this.tDictTerms.Visible = false;
        	this.tGenocard.Visible = true;
        }
        
        public void Dictionary_Click(object sender, EventArgs args) {
        	pCurrentItem.Location = new Point(-1, 148);
        	this.lAboutProject.Visible = false;
        	this.tGenocard.Visible = false;
        	this.tDictTerms.Visible = true;
        }
        
        public void AboutProject_Click(object sender, EventArgs args) {
        	pCurrentItem.Location = new Point(-1, 225);
        	this.tGenocard.Visible = false;
        	this.tDictTerms.Visible = false;
        	this.lAboutProject.Visible = true;
        }
        
        public string DictPath = @"C:\Users\fdshfgas\Documents\_USB_\Разработки 2020\App 2020\GenocardApp\Словарь терминов.txt";
        
        // Путь к файлу "Список генов.txt"
        public string listGenesPath = @"C:\Users\fdshfgas\Documents\_USB_\Разработки 2020\App 2020\GenocardApp\Список генов.txt";
        // Возвращает true если ген с данным названием есть в базе, иначе false
        private bool IsExistGeneInBase(string genename) {
        	string sgenes = null;
        	using (StreamReader sr = new StreamReader(listGenesPath, Encoding.Default)) {
        		sgenes = sr.ReadToEnd();
        	}
        	
        	bool isExist = false;
        	string[] genes = sgenes.Split('\n');
        	
        	foreach(string gene in genes) {
				isExist |= gene == genename + '\r' || gene == genename;
        	}
        	return isExist;
        }
        
        private string ReturnInfoAboutGene(string genename) {
			return IsExistGeneInBase(genename) ? GeneApi.GetGeneInfo(genename) : "Нет данных по этому запросу";
        }
        
        public void SearchInfo_Click(object sender, EventArgs args) {
        	if (this.tGenocard.Visible) {
        		string genename = tSearch.Text;
        		tGenocard.Text = ReturnInfoAboutGene(genename);
        	}
        	
        	if (this.tDictTerms.Visible) {
        		string request = this.tSearch.Text;
        		string text = GetTextFromFile(DictPath);
        		this.tDictTerms.Text = text;
        	}
        }
        
		#endregion
		
		public GenocardForm() {
			this.panelHeader.BackColor = Color.FromArgb(55,61,74);
	        this.panelHeader.BorderStyle = BorderStyle.FixedSingle;
	        this.panelHeader.Controls.Add(this.lTitle);
	        this.panelHeader.Controls.Add(this.pCollapse);
	        this.panelHeader.Controls.Add(this.pExpand);
	        this.panelHeader.Controls.Add(this.pClose);
	        this.panelHeader.Controls.Add(this.pTitle);
	        this.panelHeader.Dock = DockStyle.Top;
	        this.panelHeader.Location = new Point(0, 0);
	        this.panelHeader.Size = new Size(855, 39);
	        this.panelHeader.MouseDown += MouseDown_Click;
			this.panelHeader.MouseMove += MouseMove_Click;
			this.panelHeader.MouseUp += MouseUp_Click;
	
	        this.panelMenu.BackColor = Color.DarkSlateGray;
	        this.panelMenu.BorderStyle = BorderStyle.FixedSingle;
	        this.panelMenu.Controls.Add(this.pCurrentItem);
	        this.panelMenu.Controls.Add(this.bExit);
	        this.panelMenu.Controls.Add(this.bAboutProject);
	        this.panelMenu.Controls.Add(this.bDictionary);
	        this.panelMenu.Controls.Add(this.bGenocard);
	        this.panelMenu.Controls.Add(this.pictureLogo);
	        this.panelMenu.Controls.Add(this.linkGenocard);
	        this.panelMenu.Controls.Add(this.lCopyright);
	        this.panelMenu.Dock = DockStyle.Left;
	        this.panelMenu.Location = new Point(0, 39);
	        this.panelMenu.Size = new Size(208, 474);
	        
			this.linkGenocard.Width = 207;
	        this.linkGenocard.Height = 20;
	        this.linkGenocard.BackColor = Color.DarkKhaki;
	        this.linkGenocard.Anchor = AnchorStyles.Bottom;
	        this.linkGenocard.ForeColor = Color.White;
	        this.linkGenocard.Location = new Point(0, 420);
	        this.linkGenocard.Font = new Font("Consolas", 10, FontStyle.Regular, GraphicsUnit.Point, (Byte)204);
	        this.linkGenocard.Text = "https://www.genokarta.ru/";
	        this.linkGenocard.Click += (o,e) => System.Diagnostics.Process.Start(@"https://www.genokarta.ru/");
	        
	        this.lCopyright.Width = 207;
	        this.lCopyright.Height = 30;
	        this.lCopyright.BackColor = Color.FromArgb(46,48,49);
	        this.lCopyright.Anchor = AnchorStyles.Bottom;
	        this.lCopyright.ForeColor = Color.White;
	        this.lCopyright.Location = new Point(0, 440);
	        this.lCopyright.Font = new Font("Consolas", 10, FontStyle.Regular, GraphicsUnit.Point, (Byte)204);
	        this.lCopyright.Text = "Copyright(C) All rights reserved.";
	
	        this.panelContent.BackColor = Color.FromArgb(46,48,49);
	        this.panelContent.BackgroundImageLayout = ImageLayout.Stretch;
	        this.panelContent.BorderStyle = BorderStyle.FixedSingle;
	        this.panelContent.Controls.Add(this.panelInfo);
	        this.panelContent.Controls.Add(this.panelSeparator);
	        this.panelContent.Controls.Add(this.bSearch);
	        this.panelContent.Controls.Add(this.tSearch);
	        this.panelContent.Controls.Add(this.lSearch);
	        this.panelContent.Dock = DockStyle.Fill;
	        this.panelContent.Location = new Point(208, 39);
	        this.panelContent.Size = new Size(647, 474);
	        
	        this.tDictTerms.BackColor = Color.FromArgb(46,48,49);
	        this.tDictTerms.BorderStyle = BorderStyle.FixedSingle;
	        this.tDictTerms.Font = new Font("Consolas", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (Byte)204);
	        this.tDictTerms.ForeColor = Color.White;
	        this.tDictTerms.Dock = DockStyle.Fill;
	        this.tDictTerms.Size = new Size(254, 44);
	        this.tDictTerms.Visible = false;
	        this.tDictTerms.Multiline = true;
	        this.tDictTerms.ShortcutsEnabled = true;
	        this.tDictTerms.AcceptsTab = true;
	        this.tDictTerms.ScrollBars = ScrollBars.Both;
	
	        this.lAboutProject.BackColor = Color.FromArgb(46,48,49);
	        this.lAboutProject.Dock = DockStyle.Fill;
	        this.lAboutProject.ForeColor = Color.White;
	        this.lAboutProject.Location = new Point(208, 39);
	        this.lAboutProject.Font = new Font("Century Gothic", 12, FontStyle.Regular, GraphicsUnit.Point, (Byte)204);
	        this.lAboutProject.Visible = false;
	        this.lAboutProject.Text = GetTextFromFile(@"C:\Users\fdshfgas\Documents\_USB_\Разработки 2020\App 2020\GenocardApp\О проекте.txt");
	        
	        this.pictureLogo.BackgroundImage = Image.FromFile(ImagePath + "mainlogo.png");
	        this.pictureLogo.BackgroundImageLayout = ImageLayout.Stretch;
	        this.pictureLogo.Dock = DockStyle.Top;
	        this.pictureLogo.Location = new Point(0, 0);
	        this.pictureLogo.Size = new Size(206, 69);
	
	        this.bGenocard.Dock = DockStyle.Top;
	        this.bGenocard.FlatAppearance.BorderColor = Color.Teal;
	        this.bGenocard.FlatAppearance.MouseDownBackColor = Color.DarkCyan;
	        this.bGenocard.FlatAppearance.MouseOverBackColor = Color.DarkCyan;
	        this.bGenocard.FlatStyle = FlatStyle.Flat;
	        this.bGenocard.Font = new Font("Century Gothic", 12, FontStyle.Regular, GraphicsUnit.Point, (Byte)204);
	        this.bGenocard.ForeColor = Color.White;
	        this.bGenocard.Location = new Point(0, 69);
	        this.bGenocard.Size = new Size(206, 77);
	        this.bGenocard.Text = "Генокарта";
	        this.bGenocard.UseVisualStyleBackColor = true;
	        this.bGenocard.Image = Image.FromFile(ImagePath + "genocard.png");
	        this.bGenocard.ImageAlign = ContentAlignment.MiddleLeft;
	        this.bGenocard.TextAlign = ContentAlignment.MiddleCenter;
	        this.bGenocard.Click += Genocard_Click;
	
	        this.bDictionary.Dock = DockStyle.Top;
	        this.bDictionary.FlatAppearance.BorderColor = Color.Teal;
	        this.bDictionary.FlatAppearance.MouseDownBackColor = Color.DarkCyan;
	        this.bDictionary.FlatAppearance.MouseOverBackColor = Color.DarkCyan;
	        this.bDictionary.FlatStyle = FlatStyle.Flat;
	        this.bDictionary.Font = new Font("Century Gothic", 12, FontStyle.Regular, GraphicsUnit.Point, (Byte)204);
	        this.bDictionary.ForeColor = Color.White;
	        this.bDictionary.Location = new Point(0, 146);
	        this.bDictionary.Size = new Size(206, 77);
	        this.bDictionary.Text = "Словарь";
	        this.bDictionary.UseVisualStyleBackColor = true;
	        this.bDictionary.Image = Image.FromFile(ImagePath + "dictionary.png");
	        this.bDictionary.ImageAlign = ContentAlignment.MiddleLeft;
	        this.bDictionary.TextAlign = ContentAlignment.MiddleCenter;
	        this.bDictionary.Click += Dictionary_Click;
	
	        this.bAboutProject.Dock = DockStyle.Top;
	        this.bAboutProject.FlatAppearance.BorderColor = Color.Teal;
	        this.bAboutProject.FlatAppearance.MouseDownBackColor = Color.DarkCyan;
	        this.bAboutProject.FlatAppearance.MouseOverBackColor = Color.DarkCyan;
	        this.bAboutProject.FlatStyle = FlatStyle.Flat;
	        this.bAboutProject.Font = new Font("Century Gothic", 12, FontStyle.Regular, GraphicsUnit.Point, (Byte)204);
	        this.bAboutProject.ForeColor = Color.White;
	        this.bAboutProject.Location = new Point(0, 223);
	        this.bAboutProject.Size = new Size(206, 77);
	        this.bAboutProject.Text = "О проекте";
	        this.bAboutProject.UseVisualStyleBackColor = true;
	        this.bAboutProject.Image = Image.FromFile(ImagePath + "about.png");
	        this.bAboutProject.ImageAlign = ContentAlignment.MiddleLeft;
	        this.bAboutProject.TextAlign = ContentAlignment.MiddleCenter;
	        this.bAboutProject.Click += AboutProject_Click;
	
	        this.bExit.Dock = DockStyle.Top;
	        this.bExit.FlatAppearance.BorderColor = Color.Teal;
	        this.bExit.FlatAppearance.MouseDownBackColor = Color.DarkCyan;
	        this.bExit.FlatAppearance.MouseOverBackColor = Color.DarkCyan;
	        this.bExit.FlatStyle = FlatStyle.Flat;
	        this.bExit.Font = new Font("Century Gothic", 12, FontStyle.Regular, GraphicsUnit.Point, (Byte)204);
	        this.bExit.ForeColor = Color.White;
	        this.bExit.Location = new Point(0, 300);
	        this.bExit.Size = new Size(206, 60);
	        this.bExit.Text = "Выход";
	        this.bExit.UseVisualStyleBackColor = true;
	        this.bExit.Image = Image.FromFile(ImagePath + "exit.png");
	        this.bExit.ImageAlign = ContentAlignment.MiddleLeft;
	        this.bExit.TextAlign = ContentAlignment.MiddleCenter;
	        this.bExit.Click += (o,e) => this.Close();
	
	        this.pTitle.BackgroundImage = Image.FromFile(ImagePath + "pLogo.png");
	        this.pTitle.BackgroundImageLayout = ImageLayout.Stretch;
	        this.pTitle.Location = new Point(3, 6);
	        this.pTitle.Size = new Size(29, 26);
	
	        this.pClose.BackgroundImage = Image.FromFile(ImagePath + "close.png");
	        this.pClose.BackgroundImageLayout = ImageLayout.Stretch;
	        this.pClose.Dock = DockStyle.Right;
	        this.pClose.Location = new Point(814, 0);
	        this.pClose.Size = new Size(39, 37);
	        this.pClose.Click += CloseForm_Click;
	        this.pClose.MouseHover += (o,e) => this.pClose.BackgroundImage = Image.FromFile(ImagePath + "close_hover.png");
	        this.pClose.MouseLeave += (o,e) => this.pClose.BackgroundImage = Image.FromFile(ImagePath + "close.png");
	
	        this.pExpand.BackgroundImage = Image.FromFile(ImagePath + "expand.png");
	        this.pExpand.BackgroundImageLayout = ImageLayout.Stretch;
	        this.pExpand.Dock = DockStyle.Right;
	        this.pExpand.Location = new Point(775, 0);
	        this.pExpand.Size = new Size(39, 37);
	        this.pExpand.Click += ExpandForm_Click;
	        this.pExpand.MouseHover += (o,e) => this.pExpand.BackgroundImage = Image.FromFile(ImagePath + "expand_hover.png");
	        this.pExpand.MouseLeave += (o,e) => this.pExpand.BackgroundImage = Image.FromFile(ImagePath + "expand.png");
	
	        this.pCollapse.BackgroundImage = Image.FromFile(ImagePath + "collapse.png");
	        this.pCollapse.BackgroundImageLayout = ImageLayout.Stretch;
	        this.pCollapse.Dock = DockStyle.Right;
	        this.pCollapse.Location = new Point(736, 0);
	        this.pCollapse.Size = new Size(39, 37);
	        this.pCollapse.Click += CollapseForm_Click;
	        this.pCollapse.MouseHover += (o,e) => this.pCollapse.BackgroundImage = Image.FromFile(ImagePath + "collapse_hover.png");
	        this.pCollapse.MouseLeave += (o,e) => this.pCollapse.BackgroundImage = Image.FromFile(ImagePath + "collapse.png");
	
	        this.lSearch.Font = new Font("Century Gothic", 12, FontStyle.Bold, GraphicsUnit.Point, (Byte)204);
	        this.lSearch.ForeColor = SystemColors.ButtonFace;
	        this.lSearch.Location = new Point(20, 13);
	        this.lSearch.Size = new Size(74, 24);
	        this.lSearch.Text = "Поиск:";
	
	        this.tSearch.BackColor = Color.FromArgb(46,48,49);
	        this.tSearch.BorderStyle = BorderStyle.FixedSingle;
	        this.tSearch.Font = new Font("Century Gothic", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (Byte)204);
	        this.tSearch.ForeColor = Color.White;
	        this.tSearch.Location = new Point(100, 8);
	        this.tSearch.Size = new Size(312, 31);
	        
	        this.bSearch.BackgroundImage = Image.FromFile(ImagePath + "search_default.png");
	        this.bSearch.BackgroundImageLayout = ImageLayout.Stretch;
	        this.bSearch.FlatAppearance.BorderSize = 0;
	        this.bSearch.FlatStyle = FlatStyle.Flat;
	        this.bSearch.Location = new Point(418, 7);
	        this.bSearch.Size = new Size(35, 34);
	        this.bSearch.UseVisualStyleBackColor = true;
	        this.bSearch.Click += SearchInfo_Click;
	        this.bSearch.MouseHover += (o,e) => this.bSearch.BackgroundImage = Image.FromFile(ImagePath + "search.png");;
	        this.bSearch.MouseLeave += (o,e) => this.bSearch.BackgroundImage = Image.FromFile(ImagePath + "search_default.png");;
	 
	        this.panelSeparator.BackColor = Color.Teal;
	        this.panelSeparator.BorderStyle = BorderStyle.FixedSingle;
	        this.panelSeparator.Location = new Point(5, 45);
	        this.panelSeparator.Size = new Size(588, 2);
	
	        this.lTitle.Font = new Font("Century Gothic", 12, FontStyle.Regular, GraphicsUnit.Point, (Byte)204);
	        this.lTitle.ForeColor = SystemColors.ButtonFace;
	        this.lTitle.Location = new Point(38, 8);
	        this.lTitle.Size = new Size(178, 24);
	        this.lTitle.Text = "GenocardApp v1.0";
	
	        this.panelInfo.AutoScroll = true;
	        this.panelInfo.BackColor = Color.FromArgb(55,55,55);
	        this.panelInfo.BorderStyle = BorderStyle.FixedSingle;
	        this.panelInfo.Controls.Add(this.tGenocard);
	        this.panelInfo.Controls.Add(this.lAboutProject);
	        this.panelInfo.Controls.Add(this.tDictTerms);
	        this.panelInfo.Location = new Point(5, 55);
	        this.panelInfo.Size = new Size(637, 414);
	        this.panelInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
	 
	        this.tGenocard.BackColor = Color.FromArgb(46,48,49);
	        this.tGenocard.BorderStyle = BorderStyle.FixedSingle;
	        this.tGenocard.Font = new Font("Consolas", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (Byte)204);
	        this.tGenocard.ForeColor = Color.White;
	        this.tGenocard.Multiline = true;
	        this.tGenocard.ScrollBars = ScrollBars.Vertical;
	        this.tGenocard.Dock = DockStyle.Fill;
	        this.tGenocard.Location = new Point(100, 8);
	        this.tGenocard.Size = new Size(312, 31);
	        
	        this.pCurrentItem.BackColor = Color.Teal;
	        this.pCurrentItem.BorderStyle = BorderStyle.FixedSingle;
	        this.pCurrentItem.Location = new Point(-1, 69);
	        this.pCurrentItem.Size = new Size(10, 75);
	        
	        this.ClientSize = new Size(855, 513);
	        this.Controls.Add(this.panelContent);
	        this.Controls.Add(this.panelMenu);
	        this.Controls.Add(this.panelHeader);
	        this.FormBorderStyle = FormBorderStyle.None;
		}
	}
	
	class Program
	{
		public static GenocardForm genocardForm = new GenocardForm();
		public static void Main(string[] args)
		{
			Application.Run(genocardForm);
		}
	}
}
