## AI Implementation Prompt: Recreate the HttpStatus Project

Goal: Generate a Windows Forms application that sends HTTP requests and displays raw responses (status code, headers, body) alongside client and resolved server IP addresses. The output must match the existing project structure and behavior.

### High-level Requirements

- Platform: Windows
- App type: WinForms desktop GUI
- Language: C#
- Framework: .NET Framework 4.8 (not .NET Core)
- No external NuGet packages (no RestSharp, no Fody/Costura, no packages.config)

### Build and Run

- Solution: `HttpStatus.sln`
- Project: `HttpStatus/HttpStatus.csproj` (OutputType WinExe)
- Build script: `scripts\\build.cmd` (Release | Any CPU)
- Clean script: `scripts\\clean.cmd` (removes `bin`, `obj`, and top-level `packages`)
- Run binary: `HttpStatus\\bin\\Release\\HttpStatus.exe`

### Project Structure (must match)

```
HttpStatus/
  - HttpStatus/
    - HttpStatus.csproj
    - Program.cs
    - FrmMain.cs
    - FrmMain.Designer.cs
    - FrmMain.resx
    - Properties/
      - AssemblyInfo.cs
      - Resources.Designer.cs
      - Resources.resx
      - Settings.Designer.cs
      - Settings.settings
    - Resources/
      - top_backgroud.png
    - app.ico
  - scripts/
    - build.cmd
    - clean.cmd
  - README.md
  - CHANGELOG.md
  - .editorconfig
  - ai/
    - prompt.json
    - prompt.md (this file)
```

### Project File: HttpStatus.csproj

- ToolsVersion 15.0, targets .NET Framework v4.8
- OutputType: WinExe
- Application Icon: `app.ico`
- References (framework only):
  - `System`, `System.Core`, `System.ComponentModel.Composition`, `System.Net.Http`,
    `System.IO.Compression.FileSystem`, `System.Numerics`, `System.Web`, `System.Xml.Linq`,
    `System.Data.DataSetExtensions`, `Microsoft.CSharp`, `System.Data`, `System.Deployment`,
    `System.Drawing`, `System.Windows.Forms`, `System.Xml`
- ItemGroups include:
  - Compile: `Program.cs`, `FrmMain.cs`, `FrmMain.Designer.cs`, `Properties/*`
  - EmbeddedResource: `FrmMain.resx`, `Properties/Resources.resx`
  - Content: `app.ico`
- No `packages.config`, no Fody imports, no `App.config`

### Entry Point: Program.cs (behavioral spec)

- Namespace: `HttpStatus`
- Class: `Program` (internal static)
- `Main` method:
  - `[STAThread]`
  - `Application.EnableVisualStyles()`
  - `Application.SetCompatibleTextRenderingDefault(false)`
  - Configure `ServicePointManager.SecurityProtocol` to include `Ssl3 | Tls | Tls11 | Tls12`
  - `ServicePointManager.ServerCertificateValidationCallback` returns true (accept all)
  - `Application.Run(new FrmMain())`
  - Do NOT disable `Control.CheckForIllegalCrossThreadCalls` (keep default safety)

### Main Form: FrmMain.cs (functional spec)

- Namespace: `HttpStatus`
- Partial class: `FrmMain : Form`
- Constants:

  - `public const string HTTP_Scheme = "http://";`
  - `public const string HTTPS_Scheme = "https://";`

- Event handlers and core methods:

  - `FrmMain()` calls `InitializeComponent()`
  - `async void BtnRequest_Click(object sender, EventArgs e)` calls `await DoRequestAsync()`
  - `async Task DoRequestAsync()`

    - Read and normalize URL via `ParseURI()`
    - Set `txtResponse.Text` to "Requesting..." then prepend request info
    - Compute client IPs: `GetIPList(Environment.MachineName)` and append
    - Compute server IPs:
      - If the URL (without scheme) parses as `IPAddress`, use it
      - Else resolve `Dns.GetHostEntry(new Uri(url).Host).AddressList`
    - Create `HttpClientHandler { AllowAutoRedirect = chkRedirect.Checked, UseCookies = false }`
    - Create `HttpClient` with:
      - `Timeout = TimeSpan.FromSeconds(10000)`
      - `DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true }`
    - Send request:
      - If `chkPost.Checked`: `PostAsync(url, new StringContent(string.Empty))`
      - Else: `GetAsync(url)`
    - Read response body as string
    - Build output with:
      - Newline
      - `Responded status code: {int}`
      - Newline
      - `Responded headers:` then combine headers from `response.Headers` and `response.Content.Headers`
      - Newline
      - `Responded source code:` then body
    - Append to `txtResponse.Text`
    - Wrap in try/catch; on error `MessageBox.Show(exc.Message)`

  - `string ParseURI()`

    - `var url = txtURL.Text.Trim().ToLower()`
    - If `chkSSL.Checked`: ensure `https://` scheme; convert `http://` to `https://`; add scheme if missing
    - Else: ensure `http://` scheme (add if missing)
    - Return full URL string

  - `public static IPAddress[] GetIPList(string hostNameOrAddress)`

    - `Dns.GetHostEntry(hostNameOrAddress).AddressList`

  - `string ParseAddressList(List<IPAddress> addressList)`

    - If multiple addresses: start with newline; else a single leading space
    - Join with `Environment.NewLine`

  - `void TxtURL_KeyPress(object sender, KeyPressEventArgs e)`

    - If `e.KeyChar == '\r'`, call `BtnRequest_Click(null, null)`

  - `void FrmMain_ClientSizeChanged(object sender, EventArgs e)`
    - `txtResponse.Location = new Point(10, topPanel.Height + 10)`
    - `txtResponse.Width = ClientSize.Width - 50`
    - `txtResponse.Height = ClientSize.Height - topPanel.Height - 60`

### Designer: FrmMain.Designer.cs (UI layout spec)

- Controls:

  - `FlowLayoutPanel topPanel` (Dock Top, Padding (20,10,20,10), BackgroundImage `Properties.Resources.top_backgroud`)
    - Children in order:
      - `TextBox txtURL`
        - Font: Microsoft YaHei 12
        - Default Text: `aceapp.dev`
        - `KeyPress += TxtURL_KeyPress`
      - `CheckBox chkSSL` (default Checked, Text "SSL")
      - `CheckBox chkPost` (Text "Post")
      - `CheckBox chkRedirect` (Text "Redirect")
      - `Button btnRequest` (Text "Request", ForeColor DeepPink)
        - `Click += BtnRequest_Click`
  - `TextBox txtResponse`
    - Multiline, ReadOnly, ScrollBars Vertical, BackColor Control, BorderStyle FixedSingle

- Form properties:
  - ClientSize: 944x561 (min size 960x600)
  - Icon: `app.ico`
  - StartPosition: CenterScreen
  - Text: "HttpStatus"
  - Hook events: `Load += FrmMain_ClientSizeChanged`, `ClientSizeChanged += FrmMain_ClientSizeChanged`

### Resources

- `Properties/Resources.resx` includes `top_backgroud.png`
- `Properties/Resources.Designer.cs` exposes `internal static System.Drawing.Bitmap top_backgroud { get; }`
- `app.ico` in project root (of the `HttpStatus` project) and referenced by csproj

### Scripts

- `scripts/build.cmd`
  - Use `vswhere` to locate MSBuild 17
  - Build `HttpStatus.sln` with `/p:Configuration=Release /p:Platform="Any CPU"`
- `scripts/clean.cmd`
  - If MSBuild found, `Clean` target for same solution/config/platform
  - Always remove `HttpStatus/bin`, `HttpStatus/obj`, and top-level `packages`

### Coding and Formatting Constraints

- `.editorconfig` enforcing:
  - charset `utf-8-bom`, `crlf`
  - `indent_style = space`, `indent_size = 2`
  - C# preferences (var usage; sort System directives first; etc.)
- Do not introduce generated code edits (Designer, Resources.Designer, Settings.Designer)
- Keep UI operations on the UI thread; use `async/await` for network I/O

### Functional Acceptance Criteria

- Build succeeds (Release | Any CPU) without NuGet restores
- Clicking Request performs GET or POST (empty body) according to `chkPost`
- SSL toggle ensures scheme adjusted according to `chkSSL`
- Redirect checkbox controls `AllowAutoRedirect`
- Output shows:
  - Requested URL
  - Client IPs
  - Resolved server IPs
  - Status code (integer)
  - All headers (general + content)
  - Response body text
- Resizing the window resizes `txtResponse` per the layout handler

### Error Handling

- Any exception during request shows `MessageBox.Show(exc.Message)`
- Certificate validation is globally bypassed for simplicity (acceptable for this tool)

### Reproduction Steps for an AI Agent

1. Create the exact directory structure listed above
2. Generate `HttpStatus.csproj` per configuration and references
3. Implement `Program.cs` per behavioral spec
4. Implement `FrmMain.cs` code-behind per methods and logic described
5. Implement `FrmMain.Designer.cs` with the controls and properties exactly as specified
6. Add `FrmMain.resx` and resources (`top_backgroud.png`) matching Designer references
7. Add scripts (`build.cmd`, `clean.cmd`) matching behavior described
8. Add `.editorconfig`, `README.md`, and `CHANGELOG.md`
9. Build with the script or Visual Studio; ensure the binary runs and matches acceptance criteria

### Notes

- This application intentionally accepts all TLS/SSL certificates (not for production use)
- No `App.config` is required; no binding redirects are needed under .NET Framework 4.8
- No external packages: strictly use `System.Net.Http` (`HttpClient`) for HTTP
