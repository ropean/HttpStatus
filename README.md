## HttpStatus

A lightweight Windows Forms tool to send HTTP requests and inspect raw responses (status code, headers, and body).

New rust enhanced version: [peek](<https://github.com/ropean/peek>)

### Features
- Send HTTP GET or POST requests with optional SSL and redirect following
- Displays resolved server IPs for the requested host and local machine IPs
- Shows response status code, headers, and body

### Requirements
- .NET Framework 4.8
- Windows

### Build
- Using the provided script:
  - `scripts\\build.cmd`
- Or open `HttpStatus.sln` in Visual Studio 2022 and build (Release | Any CPU recommended)

### Run
- After building, execute `HttpStatus\\bin\\Release\\HttpStatus.exe`

### Usage
1. Enter a host or URL (e.g., `example.com`)
2. Toggle options:
   - SSL: force `https://`
   - Post: send an empty-body POST instead of GET
   - Redirect: allow following redirects
3. Click Request
4. Inspect IPs, status code, headers, and response body in the output panel

### Project Notes
- HTTP layer uses `System.Net.Http.HttpClient` (no external HTTP libraries)
- No `packages.config` or Fody/Costura dependencies
- No `App.config` binding redirects necessary under .NET Framework 4.8
- Code style enforced via `.editorconfig` (2-space indentation for C#)

### Maintenance
- Clean outputs: `scripts\\clean.cmd` (also removes the top-level `packages` folder if present)
- Changelog: see `CHANGELOG.md`

