<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecordWebForm.aspx.cs" Inherits="DoctorServer.RecordWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 24%;
        }
        .my-style{
            font-family:'Myriad Pro';
            font-size: 2.5em;
            color:#a6a29d;
        }
        .my-style2{
            font-family:'Myriad Pro';
            font-size: 2.5em;
            color:#00b99f;
        }
        .my-style-div{
            margin-top: 10px;
            padding: 2% 2% 2% 2%;
            z-index: 3;
            background-color:#e9e7e5;
        }
        .my-style-div2{
            margin-top: 10px;
            padding: 2% 2% 2% 2%;
            z-index: 10;
            background-color:#d7d5c4;
            color: #333;
            filter: progid:DXImageTransform.Microsoft.Shadow(color=#909090,direction=120,strength=3); /*ie*/
            -moz-box-shadow: 2px 2px 10px #909090; /*firefox*/
            -webkit-box-shadow: 2px 2px 10px #909090; /*safari或chrome*/
            box-shadow: 2px 2px 10px #909090; /*opera或ie9*/
        }
        .my-style-hr{
            outline-width:medium;
            color:#c9c7b7;
        }
        img{
            max-width:100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color:#00cbaf;">
            <table style="background-color: #00cbaf">
                <tr>
                    <td style="text-align: center" rowspan="2" class="auto-style1">
                        <img style="align-content: center;" src="Images/selfcheck.png" alt="Alternate Text" />
                    </td>
                    <td style="font-family: 微软雅黑; font-size: 3.8em; color: #88e5d6" class="auto-style3">Diagnoses</td>
                </tr>
                <tr>
                    <td style="font-family: 'Myriad Pro'; font-size: 2.3em; color: #fdfdfc" class="auto-style2">Image Analysis</td>
                </tr>
            </table>
        </div>

        <div class="my-style-div">
            <span class="my-style">Original Images</span>
            <hr class="my-style-hr"/>
            <asp:DataList ID="dl_srcImgs" runat="server" RepeatDirection="Horizontal">
                <ItemTemplate>
                    <asp:Image BorderStyle="Solid" BorderColor="#b7b3ad" BorderWidth="3px" ImageUrl="<%# Container.DataItem.ToString()%>" runat="server" />
                </ItemTemplate>
            </asp:DataList>
        </div>

        <div class="my-style-div">
            <span class="my-style2">Analyzed Images</span>
            <hr class="my-style-hr"/>
            <asp:DataList ID="dl_dstImgs" runat="server" RepeatDirection="Horizontal">
                <ItemTemplate>
                    <asp:Image BorderStyle="Solid" BorderColor="#b7b3ad" BorderWidth="3px" ImageUrl="<%# Container.DataItem.ToString() %>" runat="server" />
                </ItemTemplate>
            </asp:DataList>
        </div>

        <div class="my-style-div2">
			<span class="my-style2">Conclusion</span>
            <hr class="my-style-hr"/>
            <asp:Label ID="lbl_conclusion" runat="server" />
        </div>

        <div class="my-style-div">
            <span class="my-style2">Comments</span>
            <hr class="my-style-hr"/>
            <asp:DataList ID="dl_comments" runat="server">
                <ItemTemplate>
                    <asp:Label Text="<%# Container.DataItem.ToString() %>" runat="server" />
                </ItemTemplate>
            </asp:DataList>
        </div>
    </form>
</body>
</html>
