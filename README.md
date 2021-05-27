# On-behalf-of flow

# Scenario

This sample demonstrates a React single-page application (SPA) calling .Net Core Web API which in turns calls another .Net Core Downstream API

## Register the downstream web API (msal-.net core-downstream) ##

1. Navigate to the Azure portal and select the Azure AD service.
2. Select the App Registrations blade on the left, then select New registration.
3. In the Register an application page that appears, enter your application's registration information:
In the Name section, enter a meaningful application name that will be displayed to users of the app, for example msal-react-downstream
Under Supported account types, select Accounts in this organizational directory only.
4. Select Register to create the application.
5. In the app's registration screen, find and note the Application (client) ID. You use this value in your app's configuration file(s) later in your code..
6. Select Save to save your changes.
7. In the app's registration screen, select the Expose an API blade to the left to open the page where you can declare the parameters to expose this app as an API for which client applications can obtain access tokens for. The first thing that we need to do is to declare the unique resource URI that the clients will be using to obtain access tokens for this API. To declare an resource URI, follow the following steps:
Select Set next to the Application ID URI to generate a URI that is unique for this app.
For this sample, accept the proposed Application ID URI (api://{clientId}) by selecting Save.
8. All APIs have to publish a minimum of one scope for the client's to obtain an access token successfully. To publish a scope, follow the following steps:
Select Add a scope button open the Add a scope screen and Enter the values as indicated below:
For Scope name, use access_downstream_api_as_user.
Select Admins and users options for Who can consent?.
For Admin consent display name type Access msal-react-downstream.
For Admin consent description type Allows the app to access msal-react-downstream as the signed-in user.
For User consent display name type Access msal-react-downstream.
For User consent description type Allow the application to access msal-react-downstream on your behalf.
Keep State as Enabled.
Select the Add scope button on the bottom to save this scope.
9. On the right side menu, select the Manifest blade.
Set accessTokenAcceptedVersion property to 2.
Click on Save.

## Configure the  downstream web API (msal-.net core-downstream) to use your app registration ##

Go to AppSettings.json file

Under Azure AD copy ClientID of msal-react-downstream app.
Also Tenantid and Domain


## Register the middle-tier web API (msal-.net core-middletier)

1. Navigate to the Azure portal and select the Azure AD service.
2. Select the App Registrations blade on the left, then select New registration.
3. n the Register an application page that appears, enter your application's registration information:
In the Name section, enter a meaningful application name that will be displayed to users of the app, for example msal-react-middletier.
Under Supported account types, select Accounts in this organizational directory only.
4. Select Register to create the application.
5. In the app's registration screen, find and note the Application (client) ID. You use this value in your app's configuration file(s) later in your code.
6. Select Save to save your changes.
7. In the app's registration screen, select the Certificates & secrets blade in the left to open the page where we can generate secrets and upload certificates.
8. In the Client secrets section, select New client secret:
Type a key description (for instance app secret),
Select one of the available key durations (In 1 year, In 2 years, or Never Expires) as per your security posture.
The generated key value will be displayed when you select the Add button. Copy the generated value for use in the steps later.
You'll need this key later in your code's configuration files. This key value will not be displayed again, and is not retrievable by any other means, so make sure to note it from the Azure portal before navigating to any other screen or blade.
9. In the app's registration screen, select the API permissions blade in the left to open the page where we add access to the APIs that your application needs.
Select the Add a permission button and then,
Ensure that the My APIs tab is selected.
In the list of APIs, select the API msal-react-downstream.
In the Delegated permissions section, select the access_downstream_api_as_user in the list. Use the search box if necessary.
Select the Add permissions button at the bottom.
10. In the app's registration screen, select the Expose an API blade to the left to open the page where you can declare the parameters to expose this app as an API for which client applications can obtain access tokens for. The first thing that we need to do is to declare the unique resource URI that the clients will be using to obtain access tokens for this API. To declare an resource URI, follow the following steps:
Select Set next to the Application ID URI to generate a URI that is unique for this app.
For this sample, accept the proposed Application ID URI (api://{clientId}) by selecting Save.
11. All APIs have to publish a minimum of one scope for the client's to obtain an access token successfully. To publish a scope, follow the following steps:
Select Add a scope button open the Add a scope screen and Enter the values as indicated below:
For Scope name, use access_middletier_api_as_user.
Select Admins and users options for Who can consent?.
For Admin consent display name type Access msal-react-middletier.
For Admin consent description type Allows the app to access msal-react-middletier as the signed-in user.
For User consent display name type Access msal-react-middletier.
For User consent description type Allow the application to access msal-react-middletier on your behalf.
Keep State as Enabled.
Select the Add scope button on the bottom to save this scope.
12. On the right side menu, select the Manifest blade.
Set accessTokenAcceptedVersion property to 2.
Click on Save.

## Configure the  downstream web API (msal-.netcore-middletier) to use your app registration ##

Go to AppSettings.json file

Under Azure AD copy ClientID of msal-react-downstream app.
Also Tenantid and Domain

Under DownStreamAPI replace ScopeForAccessToken with

{api://clientidofdownstreamapi/access_downstream_api_as_user}



# Register the SPA app (msal-react-spa)
1. Navigate to the Azure portal and select the Azure AD service.
2. Select the App Registrations blade on the left, then select New registration.
3. In the Register an application page that appears, enter your application's registration information:
In the Name section, enter a meaningful application name that will be displayed to users of the app, for example msal-react-spa.
Under Supported account types, select Accounts in this organizational directory only.
In the Redirect URI (optional) section, select Single-page application in the combo-box and enter the following redirect URI: http://localhost:3000/.
4. Select Register to create the application.
5. In the app's registration screen, find and note the Application (client) ID. You use this value in your app's configuration file(s) later in your code.
6. Select Save to save your changes.
7. In the app's registration screen, select the API permissions blade in the left to open the page where we add access to the APIs that your application needs.
Select the Add a permission button and then,
Ensure that the My APIs tab is selected.
In the list of APIs, select the API msal-react-middletier.
In the Delegated permissions section, select the access_middletier_api_as_user in the list. Use the search box if necessary.
Select the Add permissions button at the bottom.

## Configure the  downstream web API (msal-react-spa) to use your app registration ##

replace client id with clientId of msal-react-spa
authority as {https://login.microsoftonline.com/tenantid}

Under apiHello replace scope with api://{clientid of msal-react-middletier}/access_as_user 

## How to Run ##

Navigate to OnBehalfOfFlow/spa

Run below commands

npm install 
set HTTPS=true
npm start

In Authconfig.js pls add port of middletier api in endpoint section

Run NetCoreAPI and downStreamAPI using IIS Express

In NetcoreAPI pls add port of Downstream API in endpoint section


