using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpStatus
{
  public partial class FrmMain : Form
  {
    public FrmMain()
    {
      InitializeComponent();
    }

    void BtnRequest_Click(object sender, EventArgs e)
    {
      Task.Factory.StartNew(DoRequest);
    }

    void DoRequest()
    {
      try
      {
        string url = ParseURI();

        txtResponse.Text = "Requesting...";

        txtResponse.Text = "Your IP(s):" + GetIPList(Environment.MachineName) + Environment.NewLine + Environment.NewLine;

        txtResponse.Text += "Responded IP(s):" + GetIPList(new Uri(url).Host) + Environment.NewLine;

        var client = new RestClient(url)
        {
          FollowRedirects = false,
          CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore),
          Timeout = 10000
        };

        var request = new RestRequest(chkPost.Checked ? Method.POST : Method.GET);

        var response = client.Execute(request);

        List<string> lines = new List<string>
        {
          Environment.NewLine,
          $"Responded status code: {Convert.ToInt32(response.StatusCode)}",
          Environment.NewLine,
          "Responded headers:",
        };

        lines.AddRange(response.Headers.Select(d => $"{d.Name}: {d.Value}"));

        lines.Add(Environment.NewLine);

        lines.Add("Responded source code:");

        lines.Add(response.Content);

        txtResponse.Text += string.Join(Environment.NewLine, lines);
      }
      catch (Exception exc)
      {
        MessageBox.Show(exc.Message);
      }
    }

    string HTTP_Scheme = "http://";
    string HTTPS_Scheme = "https://";

    string ParseURI()
    {
      var url = txtURL.Text.Trim().ToLower();

      if (chkSSL.Checked)
      {
        if (url.StartsWith(HTTP_Scheme))
        {
          url = url.Replace(HTTP_Scheme, HTTPS_Scheme);
        }
        else if (url.StartsWith(HTTPS_Scheme))
        {

        }
        else
        {
          url = HTTPS_Scheme + url;
        }
      }
      else
      {
        if (url.StartsWith(HTTP_Scheme))
        {

        }
        else if (url.StartsWith(HTTPS_Scheme))
        {

        }
        else
        {
          url = HTTP_Scheme + url;
        }
      }

      return url;
    }

    public static string GetIPList(string hostNameOrAddress)
    {
      IPHostEntry hostEntry = Dns.GetHostEntry(hostNameOrAddress);

      var addressList = hostEntry.AddressList.Select(d => d.ToString()).ToList();

      return (addressList.Count > 1 ? Environment.NewLine : " ") + string.Join(Environment.NewLine, addressList);
    }

    void TxtURL_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
      {
        BtnRequest_Click(null, null);
      }
    }

    private void FrmMain_ClientSizeChanged(object sender, EventArgs e)
    {
      txtResponse.Location.Offset(10, topPanel.Height + 10);

      txtResponse.Width = this.Width - 50;

      txtResponse.Height = this.Height - topPanel.Height - 60;
    }
  }
}