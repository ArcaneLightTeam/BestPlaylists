using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestPlaylists.WebForms.UserControls.RatingControl
{
    public class RatingEventArgs : EventArgs
    {
        public RatingEventArgs(int dataId, int ratingValue)
        {
            this.DataId = dataId;
            this.RatingValue = ratingValue;
        }

        public int DataId { get; set; }
        public int RatingValue { get; set; }
    }

    public partial class RatingControl : System.Web.UI.UserControl
    {
        public string UserId { get; set; }

        public int DataId { get; set; }

        public bool CanRate { get; set; }

        public string CurrentRating { get; set; }

        public delegate void RatingEventHandler(object sender, RatingEventArgs e);

        public event RatingEventHandler Rate;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.UserId != null && this.CanRate)
            {
                object[] arrayOfRating =
                    {
                            new {Name = "Select rating", Value = "0"},
                            new {Name = "1", Value = "1"},
                            new {Name = "2", Value = "2"},
                            new {Name = "3", Value = "3"},
                            new {Name = "4", Value = "4"},
                            new {Name = "5", Value = "5"},
                        };
                this.Rating.DataSource = arrayOfRating;
            }
            else
            {
                this.Rating.Visible = false;
            }

            this.Rating.DataBind();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.plRating.InnerText = this.CurrentRating;
        }


        protected void Rating_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var dropDown = sender as DropDownList;

            if (dropDown != null && this.DataId != 0)
            {
                int newRating;
                if (int.TryParse(dropDown.SelectedValue, out newRating))
                {
                    this.Rate?.Invoke(this, new RatingEventArgs(this.DataId, newRating));
                    this.Rating.Visible = false;
                }
            }
        }
    }
}