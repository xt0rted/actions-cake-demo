# actions-cake-demo

[![GitHub Actions Status For Build Workflow](https://github.com/xt0rted/actions-cake-demo/workflows/Build/badge.svg)](https://github.com/xt0rted/actions-cake-demo/actions?query=workflow%3ABuild)
[![GitHub Actions Status For Build & Deploy Workflow](https://github.com/xt0rted/actions-cake-demo/workflows/Build%20%26%20Deploy/badge.svg)](https://github.com/xt0rted/actions-cake-demo/actions?query=workflow%3A%22Build+%26+Deploy%22)
[![Dependabot Status](https://api.dependabot.com/badges/status?host=github&repo=xt0rted/actions-cake-demo)](https://dependabot.com) 

This is a sample project using GitHub Actions and [Cake](https://github.com/cake-build/cake) to build & deploy a .NET Core site to Azure App Services.

## Deployment

A deployment is started by using the command `/github deploy xt0rted/actions-cake-demo` in [Slack](https://github.com/integrations/slack) which kicks off a [GitHub Deployment](https://developer.github.com/v3/repos/deployments/).
The [deploy](/.github/workflows/deploy.yml) workflow then runs which builds, packages, and deploys the site to Azure App Services.
Finally the result of the deploy is reported back to the GitHub Deployment which is shown both in Slack and under the [Deployments](https://github.com/xt0rted/actions-cake-demo/deployments) section.

The deployed site is located at https://actions-cake-demo.azurewebsites.net.
