[![windows](https://github.com/getspacetime/spacetime/workflows/windows/badge.svg?branch=main)](https://github.com/getspacetime/spacetime/actions/workflows/windows.yml)
[![build](https://github.com/getspacetime/spacetime/workflows/build/badge.svg?event=push)](https://github.com/getspacetime/spacetime/actions/workflows/build.yml)
<div align="center">
  <a href="https://github.com/getspacetime/spacetime">
    <img src="https://user-images.githubusercontent.com/1738479/164597569-c1df8287-5d2b-434a-89fa-170e81cb6d90.png" alt="Logo" width="150" height="150">
  </a>
<h3 align="center">spacetime</h3>

  <p align="center">
    A fully featured cross-platform, cross-transport API Client, wormholes excluded.
    <br />
    <a href="https://github.com/spacetimedotnet/spacetime"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/spacetimedotnet/spacetime/issues/new?assignees=&labels=&template=bug_report.md&title=">Report Bug</a>
    ·
    <a href="https://github.com/spacetimedotnet/spacetime/issues/new?assignees=&labels=&template=feature_request.md&title=">Request Feature</a>
  </p>
</div>
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

## About The Project

![spacetime banner](https://user-images.githubusercontent.com/1738479/164606891-cf50aa1b-4d2b-400c-8832-d4d74b664c0e.png)

A fully featured* cross-platform, cross-transport API Client, wormholes excluded. 

Spacetime is proudly built with .NET MAUI Blazor.

*not quite fully featured yet*


<p align="right">(<a href="#top">back to top</a>)</p>

### Built With

* [.NET MAUI Blazor](https://docs.microsoft.com/en-us/aspnet/core/blazor/hybrid/tutorials/maui?view=aspnetcore-6.0)
* [LiteDB](https://www.litedb.org/)
* [TailwindCSS](https://tailwindcss.com/)
* [Flurl](https://flurl.dev/)
* [Webpack](https://github.com/webpack/webpack)
* [npm](https://www.npmjs.com/)
<p align="right">(<a href="#top">back to top</a>)</p>

## Getting started

### Prerequisites
* [Visual Studio 2022 Preview](https://visualstudio.microsoft.com/vs/preview/)
* [.NET MAUI Workload](https://docs.microsoft.com/en-us/dotnet/maui/get-started/installation) - currently RC1

<p align="right">(<a href="#top">back to top</a>)</p>

### Running locally

There are two steps to running locally. First, the front end resources (JavaScript and Tailwind) need to be bundled using Webpack.

1. Install NPM
2. Install Webpack
3. In the `wwwroot` folder, run the command `npm run dev`

Once you have run the above npm command, you can start debugging in Visual Studio. When you make changes, be sure to restart the application (hot reloading doesn't seem to work quite right yet).

### Installation

#### Windows

Use the [MSIX Installer](https://github.com/spacetimedotnet/spacetime/releases)

#### Mac
Clone the repository and run the `dotnet` command in the Spacetime project folder. **Note**: don't forget to run `npm run build` inside the `wwwroot` folder.

```
dotnet build -t:Run -f net6.0-maccatalyst
```

**Installer*
[TBD](https://github.com/spacetimedotnet/spacetime/issues/5)

#### iOS

Completely untested. Theoretically should work, but may need tweaks to configuration and/or a developer account.

[TBD](https://github.com/spacetimedotnet/spacetime/issues/6)

#### Android

Has an error during compilation. Needs investigation. 
[TBD](https://github.com/spacetimedotnet/spacetime/issues/7)

<p align="right">(<a href="#top">back to top</a>)</p>

## Roadmap
- [ ] UX
    - [ ] Responsive design
    - [ ] Keyboard shortcuts
- [ ] Core
    - [ ] User settings
    - [ ] Security settings
    - [ ] Request history
    - [ ] Data Freedom (Exports)
- [ ] Platform
    - [x] Windows
    - [ ] Mac _(in progress)_
    - [ ] Linux
    - [ ] iOS
    - [ ] Android
- [ ] REST
    - [ ] Full support for basic REST operations _(in progress)_
- [ ] gRPC
    - [ ] GRPC Explorer _(in progress)_
    - [ ] Unary support _(in progress)_
    - [ ] Streaming support
- [ ] Kafka
    - [ ] Produce messages
    - [ ] Consume messages
- [ ] Websockets

<p align="right">(<a href="#top">back to top</a>)</p>

## Contributing
Open an issue or tweet me on Twitter with any suggestions or bug reports.

### Architecture
The Spacetime MAUI Project uses [Fluxor](https://github.com/mrpmorris/Fluxor) for state management. This means all actions are kicked off using the Dispatcher and handled either in an Effect or a Reducer.

**Example:**
```
<button OnClick="Save">Save</button>
@code {
    private void Save()
    {
        Dispatcher.Dispatch(new UpdateRequestAction(Request));
    }
}
```

This will fire an action, and at the very simplest level, can be handled in a reducer method. However, if this action does anything other than mutate state in a reducer, such as an API call, this will be created in an Effect. 

All Actions, Reducers, and Effects are in the `Spacetime.Store` namespace, organized by "feature."

View the Fluxor documentation for further explanation.

<p align="right">(<a href="#top">back to top</a>)</p>

## Contact
Cody Mullins - [@codemullins](https://twitter.com/codemullins)
<p align="right">(<a href="#top">back to top</a>)</p>
