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
using Rock;
using Rock.Attribute;
using Rock.Common.Mobile.Blocks.Groups.GroupMemberList;
using Rock.Data;
using Rock.Model;
using Rock.Web.UI;
using System;
using System.ComponentModel;
using System.Web.UI;

namespace RockWeb.Plugins.MyProject.Groups
{
    [DisplayName("Group Detail")]
    [Category("My Project > Groups")]
    [Description("Displays the details for a selected group.")]

    public partial class GroupDetail : RockBlock
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            int? groupId = PageParameter( "GroupId" ).AsIntegerOrNull();

            if (groupId.HasValue)
            {
                ShowDetail( groupId.Value );
            }
            else
            {
                pnlDetails.Visible = false;
            }
        }

        protected void ShowDetail( int groupId )
        {
            using (var rockContext = new RockContext())
            {
                var group = new GroupService(rockContext).Get(groupId);

                if (group != null)
                {
                    pnlDetails.Visible = true;
                    lGroupDetails.Text = $@"
                        <h3>{group.Name}</h3>
                        <p><strong>Description:</strong> {group.Description}</p>
                        <p><strong>Date Created:</strong> {group.CreatedDateTime?.ToShortDateString()}</p>
                        <p><strong>Date Modified:</strong> {group.ModifiedDateTime?.ToShortDateString()}</p>
                        <p><strong>Group Capacity:</strong> {group.GroupCapacity}</p>
                    ";
                }
                else
                {
                    pnlDetails.Visible = false;
                }
            }
        }

    }
}