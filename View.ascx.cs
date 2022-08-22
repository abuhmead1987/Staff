﻿/*
' Copyright (c) 2022  Mohammad Hmedat
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using DotNetNuke.ComponentModel;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Services.Localization;
using System;
using System.Web.UI;

namespace Mohammad.Modules.Staff
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from StaffModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : StaffModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ModulePermissionController.CanEditModuleContent(ModuleConfiguration))
                {
                    Panel_cmd.Visible = true;
                    Panel_cmd.Enabled = true;
                }
                if (Session["currentControl" + ModuleId] != null)
                {
                    //StaffModuleBase portalModuleBase = (StaffModuleBase)((TemplateControl)this).LoadControl((string)Session["StaffAdmin" + ModuleId]);
                    StaffModuleBase portalModuleBase = (StaffModuleBase)((TemplateControl)this).LoadControl("Controls/StaffAdmin.ascx");
                    if (portalModuleBase == null)
                        return;
                    PlaceHolder1.Controls.Clear();
                    portalModuleBase.ModuleConfiguration = (ModuleConfiguration);
                    PlaceHolder1.Controls.Add(portalModuleBase);
                }
                else
                {
                    StaffModuleBase portalModuleBase = (StaffModuleBase)LoadControl("Controls/StaffPreview.ascx");
                    if (portalModuleBase == null)
                        return;
                    PlaceHolder1.Controls.Clear();
                    portalModuleBase.ModuleConfiguration = (ModuleConfiguration);
                    PlaceHolder1.Controls.Add(portalModuleBase);
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }
        //public string getFilePath(string fileID)
        //{
        //    FileInfo file = (FileInfo)ComponentBase<IFileManager, FileManager>.Instance.GetFile(Convert.ToInt32(fileID));
        //    if (file != null)
        //        return ComponentBase<IFileManager, FileManager>.Instance.GetUrl(file);
        //    else
        //        return "/DesktopModules/Staff/Images/no-thumb.png";
        //}
        //public string decodeHTML(string str)
        //{
        //    return Server.HtmlDecode(str);
        //}

        protected void btn_addRemoveStaff_Click(object sender, EventArgs e)
        {
            Session["currentControl" + ModuleId] = "Controls/StaffAdmin.ascx";
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btn_previewStaff_Click(object sender, EventArgs e)
        {
            Session["currentControl" + ModuleId] = null;
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}