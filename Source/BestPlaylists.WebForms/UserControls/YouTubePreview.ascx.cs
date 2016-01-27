namespace BestPlaylists.WebForms.UserControls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    public partial class YouTubePreview : UserControl
    {
        private Side side;
        private string eventName;
        private string assoctiatedControlId;

        public string AssociatedControlId
        {
            get
            {
                return this.assoctiatedControlId;
            }
            set
            {
                this.assoctiatedControlId = value;
            }
        }

        /// <summary>
        /// Without "on" prefix
        /// </summary>
        public string EventName
        {
            get
            {
                return eventName;
            }
            set
            {
                eventName = value;
            }
        }

        public Side Side
        {
            get
            {
                return this.side;
            }
            set
            {
                this.side = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.updatePanelVideo.Triggers.Add(new AsyncPostBackTrigger()
            {
                ControlID = this.AssociatedControlId,
                EventName = this.EventName
            });

            // find control with id: "this.AssociatedControlId" and set post-back to true: tbUrlInput.AutoPostBack = true;
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.videoFramePreview.Visible = false;
        }

        public void HideVideo()
        {
            this.videoFramePreview.Visible = false;
            this.updatePanelVideo.Update();
        }

        public void PreviewVideoWithSlider(object sender, EventArgs e)
        {
            TextBox tbUrlInput = sender as TextBox;
            if (!tbUrlInput.Text.Contains("https://www.youtube.com/watch?v="))
            {
                this.videoFramePreview.Visible = false;
                return;
            }

            string url = "https://www.youtube.com/embed/" + tbUrlInput.Text.Substring(tbUrlInput.Text.IndexOf("=") + 1);

            // This works on magic
            this.videoFramePreview.Visible = true;
            this.videoFramePreview.Attributes["src"] = url;
            this.videoFramePreview.Attributes.Add("style", "border: 5px solid rgb(51, 122, 183); border-radius: 5px;z-index: 123;");

            switch (this.Side)
            {
                case Side.Top:
                    this.videoFramePreview.Attributes.Add("class", "animation slide-to-top");
                    break;
                case Side.Left:
                    this.videoFramePreview.Attributes.Add("class", "animation slide-to-left");
                    break;
                case Side.Bottom:
                    this.videoFramePreview.Attributes.Add("class", "animation slide-to-bottom");
                    break;
                case Side.Right:
                    this.videoFramePreview.Attributes.Add("class", "animation slide-to-right");
                    break;
                default:
                    break;
            }

            this.updatePanelVideo.Update();
        }
    }
}