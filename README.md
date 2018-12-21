# ProjectBubbles

Project Bubbles is a personal initiative to build some kind of App which will easely lets a group of people know each other's if they are participating to an event/meeting. 

The start is a Xamarin cross platform app and an ASP.NET Core backend on Azure.

# First features

- Multipages applications that allows users to enter their activity/feeling for the upcoming 10 days
- UWP and Android app available
- Azure ASP.NET Core Rest API
- Azure Table Storage for data and profiles (2 tables)

# Application Log
| Date | Action type | Comments |
|------|-------------|----------|
2018-12-16|Feature|MobileAppService supports storing profiles
2018-12-01|Feature|Settings page using SQLite (Username saved locally)
2018-11-23|Feature|Multi-tabs/pages for the 10 upcoming days
2018-11-23|Feature|AppCenter Analytics and Crashes for the UWP and Android app
2018-11-19|Feature|One page for one team one day
2018/12/21|Feature|Get base64 image profil from Azure Table using IValueConverter


# Project Log
| Date | Action type | Comments |
|------|-------------|----------|
2018-11-23|DevOps|AppCenter Analytics and Crahses
2018-11-21|Feature|App working for one team several days
2018-11-20|DevOps|AppCenter for automated build
2018-11-19|Feature|First preview version (UWP and Android) working for one team one day
2018-11-17|Feature|Using Azure Table storage
2018-11-14|Feature|Project startup: One team
2018-12-13|Technical|Save/Get image for profile encoded in base64


# Automated Builds
| Platform | AppCenter builds |
|----------|----------|
| Android | [![Build status](https://build.appcenter.ms/v0.1/apps/022c2130-44cb-4aba-b3ee-f8a5eb9fb0f8/branches/master/badge)](https://appcenter.ms)|
| iOS | [![Build status](https://build.appcenter.ms/v0.1/apps/cc860c22-5117-4e62-9e03-a5b1db30b436/branches/master/badge)](https://appcenter.ms) |
| UWP | [![Build status](https://build.appcenter.ms/v0.1/apps/66656b9f-8def-4324-be8a-9fbccef23719/branches/master/badge)](https://appcenter.ms) |
