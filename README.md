[![Build](https://github.com/getspacetime/spacetime/actions/workflows/build.yaml/badge.svg)](https://github.com/getspacetime/spacetime/actions/workflows/build.yaml)
[![MacOS](https://github.com/getspacetime/spacetime/actions/workflows/mac.yml/badge.svg)](https://github.com/getspacetime/spacetime/actions/workflows/mac.yml)
[![Windows](https://github.com/getspacetime/spacetime/actions/workflows/windows.yml/badge.svg)](https://github.com/getspacetime/spacetime/actions/workflows/windows.yml)

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
  
  <b align="center">Spacetime is not production-ready and is in active development. Pre-release users are welcomed with this warning in mind. </b>
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
    <li>
      <a href="#contributing">Contributing</a>
      <ul>
        <li><a href="#architecture">Architecture</a></li>
      </ul>
    </li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

## About The Project

- [x] REST
- [x] gRPC (_we support server reflection! stop reloading protobuf files!_)
- [ ] Synchronization (_coming soon, for free!_)
- [ ] WebSockets
- [ ] Kafka

![3027DB7C-25DD-49C3-B7CA-C0F1CA823276](https://user-images.githubusercontent.com/1738479/170391560-29c4e860-e1b4-4907-803d-d88610dd446c.GIF)
<img width="1595" alt="rest api client" src="https://user-images.githubusercontent.com/1738479/169888145-1d991141-4d78-46b9-8477-3e800c2b2e41.png">
<img width="1601" alt="grpc client" src="https://user-images.githubusercontent.com/1738479/169887937-54576e10-c628-4cee-98b1-602cc97cd851.png">

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
* npm
* webpack

<p align="right">(<a href="#top">back to top</a>)</p>

### Running locally

Once all prerequisites are fulfilled, make sure to run `npm install` in the `wwwroot` folder. When you build the solution, the project will automatically run `npm run build`.

Note: hot reloading does not yet work, but it should be fixed soon.

### Installation

#### Windows

Use the [MSIX Installer](https://github.com/spacetimedotnet/spacetime/releases)

#### Mac Source
Clone the repository and run the `dotnet` command in the Spacetime project folder. 

```
dotnet build -t:Run -f net6.0-maccatalyst
```

#### Mac Binary

Download the [zip file](https://github.com/spacetimedotnet/spacetime/releases) and extract the `.app` 


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

View the [Fluxor documentation](https://github.com/mrpmorris/Fluxor/blob/master/Tutorials/02-Blazor/02A-StateActionsReducersTutorial/README.md) for further explanation.

<p align="right">(<a href="#top">back to top</a>)</p>

## Contact
Chat on [Discord](https://discord.gg/PeBbYy6WKq) or tweet me [@codemullins](https://twitter.com/codemullins)
<p align="right">(<a href="#top">back to top</a>)</p>

<img width="393" alt="MS_Startups_Celebration_Badge_Dark" src="https://user-images.githubusercontent.com/1738479/169885705-9d8eb55c-0c1d-46c1-9a28-b82887ed37d3.png">
