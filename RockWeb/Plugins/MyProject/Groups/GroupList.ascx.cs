// <copyright>
// Copyright by the Spark Development Network
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data.Entity;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

using Rock;
using Rock.Data;
using Rock.Model;
using Rock.Web.Cache;
using Rock.Web.UI.Controls;
using Rock.Attribute;
using Rock.Web.UI;

namespace RockWeb.Plugins.MyProject.Groups
{
    [DisplayName("Group List")]
    [Category("My Project > Groups")]
    [Description("Lists all active groups of type 'Small Group'.")]
    [LinkedPage("Detail Page")]
    public partial class GroupList : RockBlock
    {
        #region Base Control Methods

        protected override void OnInit( EventArgs e )
        {
            base.OnInit( e );
            gList.GridRebind += gList_GridRebind;

            this.BlockUpdated += Block_BlockUpdated;
            this.AddConfigurationUpdateTrigger(upnlContent);

        }

        protected override void OnLoad( EventArgs e )
        {
            if ( !Page.IsPostBack )
            {
                BindGrid();
            }
            base.OnLoad( e );
        }

        #endregion

        #region Events

        protected void Block_BlockUpdated( object sender, EventArgs e )
        {
            
        }

        protected void gList_GridRebind( object sender, EventArgs e )
        {
            BindGrid();
        }

        protected void gList_RowSelected(object sender, Rock.Web.UI.Controls.RowEventArgs e)
        {
            NavigateToLinkedPage( "DetailPage", "GroupId", e.RowKeyId);
        }

        #endregion

        #region Methods
        private void BindGrid()
        {
            using ( var rockContext = new RockContext() )
            {
                var groupService = new GroupService( rockContext );
                var smallGroups = groupService.Queryable()
                    .Where(g => g.IsActive && g.GroupType.Name == "Small Group")
                    .OrderBy(g => g.Name)
                    .Select(g => new
                    {
                        g.Id,
                        g.Name,
                        g.Description
                    })
                    .ToList();

                gList.DataSource = smallGroups;
                gList.DataBind();
            }
        }

        #endregion
    }
}