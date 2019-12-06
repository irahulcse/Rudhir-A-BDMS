# Rudhir-A-BDMS
It is a project under which diff donors can register and with their address we can tell them you should donate your blood at these banks.
Adding on Users who need blood don't require to register then can easily view the stock available for specific blood and can get it 
through different Hospitals via this system.Also diff Hospitals can register here and can have blood stock updated.Individual Donors can register and they will 
shown the details of all the available Blood Banks available in their state.
Admin Updates camps that are going to take place and these can be viewed by everyone.Also have a chatbot that can resolve your issues.

#### Website's Home Page:
![](https://github.com/DhruvKinger/Rudhir-A-BDMS/blob/master/Forgithub/Screenshot%20(561).png)


### Advanced Features Implemented :
#### Google Maps Integration: 
![](https://github.com/DhruvKinger/Rudhir-A-BDMS/blob/master/Forgithub/Screenshot%20(924).png)

#### ChatBot Implementation: 
![](https://github.com/DhruvKinger/Rudhir-A-BDMS/blob/master/Forgithub/Screenshot%20(926).png)
#### Microsoft Azure Database Connection:
![](https://github.com/DhruvKinger/Rudhir-A-BDMS/blob/master/Forgithub/azure.JPG)
#### Forgot Password Credentials:
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(420).png)

#### Notification: 
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(421).png)
#### Token Sent on Email For Password Change: 
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(422).png)


+ [Development](#development)
+ [Contribute](#contribute)
+ [Setup](#setup)
+ [Features](#features)
+ [Screenshots](#screenshots)

## Development
The backend of the system is developed on *[MSSQL SERVER](https://www.microsoft.com/en-au/sql-server/sql-server-downloads)* and it is free and open source. You just need to download this and installed it on your pc and it will be integrated with the Frontend in the Visual Studio 2017.
The front end is built on *[MVC]*, you can use that in Visual Studio an OpenSource Platform Developed By Microsoft.Bootstrap Files are used too[Bootstrap v2.2.2](http://bootstrapdocs.com/v2.2.2/docs/) using [jQuery](https://blog.jquery.com/2013/02/04/jquery-1-9-1-released/) and [Ajax](https://www.w3schools.com/xml/ajax_intro.asp) and basic HTML/CSS/Javascript are used.On Form Submission Sweet Alert Library of Javascript is used which gives a beautiful popup.
Entity Framework 6.0 is used with the Database First approach is followed In this project(That Is Firstly database is developed then after that Model  and then Controller and View).

## Contribute
+ For reporting bug about an incorrect file not being processed, open a new issue.
+ PRs are always welcome to improve exisiting system and documentation too.:thumbsup:

### Default Login Credentials For Admin
| Username | Password |
| ------------- | ------------- |
| dhruvkinger813@gmail.com | India99@ |

### Default Login Credentials For Teacher
| Username | Password |
| ------------- | ------------- |
| dtilak1999@gmail.com | dTilak99|

### Default Login Credentials For Coordinator
| Username | Password |
| ------------- | ------------- |
| cse1785@gmail.com | Cse1785@ |


### Technology Used
* [MVC](https://dotnet.microsoft.com/apps/aspnet/mvc) - MVC For Learning Basics
* [Ajax](https://stackoverflow.com/questions/9988634/ajax-call-into-mvc-controller-url-issue/9988672) - Calling Controller From View using Ajax
* [JSON](https://www.w3schools.com/whatis/whatis_json.asp) - JSON Library, for storing configurations
* [Entity Framework](https://stackoverflow.com/questions/16480295/linq-group-by-and-order-by-in-c-sharp) -For adding some extra functionality through OrderBy and GroupBy

## Setup

### Prerequisite: Install MSSQL 

If you don't already have the MSSQL Database Server(MSSQL Server Version 2012) installed, you will need to install it to use this project. If it is installed, you can skip to step 4.

1. Connect to your Windows server with Remote Desktop Connection.
2. From the Start Menu, open Internet Explorer.
3. Paste one of the following URLs into the address bar for the version you want to use, then press Enter. All versions are compatible with Windows Server 2008 and 2012.<br/>
[SQL Server 2008](http://download.microsoft.com/download/0/4/B/04BE03CD-EAF3-4797-9D8D-2E08E316C998/SQLEXPRWT_x64_ENU.exe)<br/>
[SQL Server 2012](http://download.microsoft.com/download/8/D/D/8DD7BDBA-CEF7-4D8E-8C16-D9F69527F909/ENU/x64/SQLEXPRWT_x64_ENU.exe)<br/>
[SQL Server 2014](http://download.microsoft.com/download/E/A/E/EAE6F7FC-767A-4038-A954-49B8B05D04EB/ExpressAndTools%2064BIT/SQLEXPRWT_x64_ENU.exe)
4. Scroll down and click Run to begin the download of SQL Server.
5. Click Yes to begin the install.
6. Click New installation or add features to an existing installation.
7. Agree to the terms for SQL Server, and proceed with the rest of the steps in the install wizard. During the wizard, make sure you perform the following steps:
When you get to the section for Server Configuration, make sure to toggle SQL Server Browser to Automatic.
When you get to the section for Database Engine Configuration, select Mixed Mode for authentication, a0nd enter a master password for your SQL Server install.

### Install Visual Studio 2017
Step 1: Before you begin installing Visual Studio:
1. Check the system requirements. These requirements help you know whether your computer supports Visual Studio 2017.
2. Apply the latest Windows updates. These updates ensure that your computer has both the latest security updates and the required system components for Visual Studio.
3. Reboot. The reboot ensures that any pending installs or updates don't hinder the Visual Studio install.
4. Free up space. Remove unneeded files and applications from your %SystemDrive% by, for example, running the Disk Cleanup app.

For questions about running previous versions of Visual Studio side by side with Visual Studio 2017<br/>See the [Visual Studio Compatibility Details](https://docs.microsoft.com/en-us/visualstudio/productinfo/vs2017-compatibility-vs#compatibility-with-previous-releases)<br/>
Step 2: Download Visual Studio
Next, download the Visual Studio bootstrapper file. To do so, choose the following button, choose the edition of Visual Studio that you want, choose Save, and then choose Open folder.<br/>
[Download Visual Studio](https://visualstudio.microsoft.com/vs/older-downloads/?utm_medium=microsoft&utm_source=docs.microsoft.com&utm_campaign=vs+2017+download
)<br/>
Step 3 - Install the Visual Studio installer<br/>
Run the bootstrapper file to install the Visual Studio Installer. This new lightweight installer includes everything you need to both install and customize Visual Studio.<br/>
1. From your Downloads folder, double-click the bootstrapper that matches or is similar to one of the following files:
  * vs_community.exe for Visual Studio Community
  * vs_professional.exe for Visual Studio Professional
  * vs_enterprise.exe for Visual Studio Enterprise
If you receive a User Account Control notice, choose Yes.
2. We'll ask you to acknowledge the Microsoft [License Terms](https://visualstudio.microsoft.com/license-terms/) and the Microsoft [Privacy Statement](https://privacy.microsoft.com/en-GB/privacystatement).<br/>Choose Continue.<br/><br/>
![](https://docs.microsoft.com/en-us/visualstudio/install/media/privacy-and-license-terms.png?view=vs-2019)<br/>
<br/>Step 4 - Choose workloads
   After the installer is installed, you can use it to customize your installation by selecting the feature sets—or workloads—that you      want. Here's how.<br/>
    <br/>1. Find the workload you want in the Installing Visual Studio screen.<br/>
 <br/>![](https://docs.microsoft.com/en-us/visualstudio/install/media/vs-installer-installing-workloads.png?view=vs-2019)<br/>
 <br/>For example, choose the ".NET desktop development" workload. It comes with the default core editor, which includes basic code        editing support for over 20 languages, the ability to open and edit code from any folder without requiring a project, and integrated    source code control.<br/>
       <br/> 2. After you choose the workload(s) you want, choose Install.
    Next, status screens appear that show the progress of your Visual Studio installation.

## Features
+ Admin can create Subjects,Teachers,Departments,Semsters(say CSE,MCA Department)and Admin have the right to create Semsters and their different Subjects.
+ Admin is more of the supreme or you can say a Senior Vice President Person who commands and monitors the progress.
+ Teacher can access its portal and he can download the format of the Question Paper to be Uploaded.After filling in the Excel Sheet with data he can Upload that File.
+ All those Questions from different teachers from different Departments and from different Subjects are saved in the Database.
+ Admin  can View all that data and now come his work to Create Question Paper for All the Deartments.
+ Admin Choose the Department,Semster,Subject and All the Questions related to those filters will be displayed.He will click on a Button and then Question Paper will be Generated on basis of Randomization Algo.
+ Now that Generated Quesion Paper can be converted into Pdf which can be shared with the Respective Departments of The University.
+ Some Extra Features are Added inn the Project is Signin with Google,Token Code for Account Verification and Rotvalia Package for Pdf Generation.

## Screenshots

#### Admin,HOD,Coordinator and Teacher Login Portal :
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(418).png)
#### Admin Panel(After Login):
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(408).png)
#### Contact Us:
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(407).png)
#### Admin Creating Departments:
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(409).png)
#### Registration Form:
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(417).png)
#### Admin Creating Semsters:
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(410).png)
#### Admin Creating Coordinators:
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(414).png)
#### Admin Updating Subjects:
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(412).png)
#### Questions Uploaded By Teacher:
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(419).png)
#### Admin Viewing Uploaded Questions:
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(413).png)
#### Excel Downloaded File:
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(431).png)
#### Password Changing Panel:
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(423).png)
#### Admin Creating Pdf Of Question Paper:
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(415).png)
#### Generated Pdf:
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(416).png)
#### Some Help to Teachers:
![](https://github.com/DhruvKinger/Curosome/blob/master/Forgithub/Screenshot%20(430).png)

## Furture Scope:
* The web application can be upgraded to a very high responsive and well designed application in the future with many addition of different features.
*	In future the web application can also support of making of circles which would include a list of members as added by the circle leader and they all can upload question sets and then at the time of generation the generated question set will be delivered to the circle leader and not anyone else and thus reducing the chance of question paper leakage. 
* The feature of rewards and quizzes can also be added later which will contain all the question from all the uploaders related to the specific subject and therefore one can go through the quiz and if answered 90% correct than can be rewarded. There are innumerable opportunities and ways to make this web application more useful, featured and fun for the general visitors. 
* This will not only help the teachers but also the students to enjoy learning and answering questions and learn along with the fun. 

