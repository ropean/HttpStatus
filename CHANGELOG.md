## Changelog

All notable changes to this project will be documented in this file.
This project adheres to Keep a Changelog and Semantic Versioning where practical.

## [Unreleased] - 2025-08-31

### Removed

- Removed `packages.config` and all NuGet-managed references from `HttpStatus.csproj`.
- Removed Fody/Costura artifacts and imports (`FodyWeavers.xml`, `FodyWeavers.xsd`).
- Removed `App.config` binding redirects (no longer needed on .NET Framework 4.8).

### Changed

- Migrated HTTP logic from RestSharp to `System.Net.Http` (`HttpClient`).
- Refactored `FrmMain` request flow to `async/await` for responsiveness.
- Improved layout sizing in `FrmMain.ClientSizeChanged`.
- Replaced application icon with `app.ico`.
- Kept cross-thread checks enabled; removed bypass in `Program.cs`.

### Added

- Added `.editorconfig` enforcing 2-space indentation for C#.
- Updated `scripts/clean.cmd` to also remove the top-level `packages` directory.

### Build

- Project builds successfully in Release | Any CPU without external packages.
