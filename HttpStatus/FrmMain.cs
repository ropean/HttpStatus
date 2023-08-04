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

        txtResponse.Text = string.Format("IP: {0}", GetIPList(url)) + Environment.NewLine;

        var client = new RestClient(url)
        {
          FollowRedirects = false,
          CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore),
          Timeout = 10000
        };

        var request = new RestRequest(Method.GET);

        var response = client.Execute(request);

        List<string> lines = new List<string>
        {
          string.Format("StatusCode: {0}", Convert.ToInt32(response.StatusCode).ToString()),
          Environment.NewLine
        };

        lines.AddRange(response.Headers.Select(d => string.Format("{0}: {1}", d.Name, d.Value)));

        lines.Add(Environment.NewLine);

        lines.Add(response.Content);

        txtResponse.Text += string.Join(Environment.NewLine, lines);
      }
      catch (Exception exc)
      {
        txtResponse.Text = exc.Message;
      }
    }

    string ParseURI()
    {
      var scheme = chkSSL.Checked ? "https://" : "http://";

      var url = txtURL.Text.Trim();

      if (url.StartsWith(scheme, StringComparison.CurrentCultureIgnoreCase))
      {
        return url;
      }

      return scheme + url;
    }

    public static string GetIPList(string url)
    {
      Uri uri = new Uri(url);

      IPHostEntry hostEntry = Dns.GetHostEntry(uri.Host);

      var addressList = hostEntry.AddressList.Select(d => d.ToString());

      return string.Join(Environment.NewLine, addressList);
    }

    void TxtURL_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
      {
        BtnRequest_Click(null, null);
      }
    }
  }
}