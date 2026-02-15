# READ:

## Here are prerequisites needed before you can push your commit/s:

1. Ensure that you are using [.NET 10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) in [Visual Studio](https://visualstudio.microsoft.com/).

2. We'll use [GunaUI](https://gunaui.com/) for our UI/UX, make sure this is also downloaded.

## Cautions/Things to consider:

1. To ensure that your files will be safe, do ***NOT*** create the files in the cloned repository, create your files in a separate directory, and then just include these files *inside* the repository when you're going to push. In this way, you can still work with your files in its own directory without risking of having the files removed when the repository gets updated.

#### What do I mean by this?

- Your created files are usually located in `C:\Users\<your-username>\source\repos`, wherein you'd probably see the repository name, and the project you made.
- To keep things safe, don't create your project inside of the cloned repository; instead, create it separately, like shown in the image.

<img width="317" height="176" alt="{32F8D14D-7F2F-48D4-AF7C-B8ABD3C0EEE0}" src="https://github.com/user-attachments/assets/47c34822-1774-4c6f-8ccb-f1ad4069881f" />

2. Create a folder for your files. As seen before, we were unplanned and we had messed up pretty badly. Creating your own folder and having your files there ensures that we work in a clean structure, even when this is pushed to the repository, it will look much more organized since we have our set of folders.

3. Create a branch. Do ***NOT*** push everything to the master branch, this is an accident waiting to happen. While we *can* revert the changes, it will eventually keep up to us when we mess up one time. Mess up one time, it'll be big time.

4. Do ***NOT*** forget to name your forms/files, it's generally harder to differentiate when we have all the same file names. As much as possible, rename them onto your assigned tasks (e.g Assessor.cs)

5. Always keep a backup of your file.

### Alright, let's move onto creating the project.

# To create a new project, do the following:

1. As the program launches, you'll usually see this window, click "Create a new project" located at the top right side indicated.
  <img width="774" height="748" alt="image" src="https://github.com/user-attachments/assets/6fc2d17f-9acb-4884-98bf-f2d6e61d450a" />

2. Now, in the search bar, type "WinForms" to only index every Windows Forms App. </br>
2.1. The next thing to consider is what to pick. This is easy to distinguish. ***NEVER*** select Windows Forms App in C# with ***.NET FRAMEWORK***, this is meant for legacy applications, and we do not need this. Only select "Windows Forms App C#"
   
   <img width="774" height="748" alt="image" src="https://github.com/user-attachments/assets/2c18e654-b81c-4892-ab77-5ed2edd4ef2a" />

4. Add a name to your project, and make sure it's distinguishable since we will be having our own set of tasks. To keep things clean, we can have a different project name and solution name as shown in the image. If you want it to have the same name, just check "Place solution and project in the same directory".
   
   <img width="774" height="748" alt="image" src="https://github.com/user-attachments/assets/f5222e7e-daca-4bd3-9013-f2af43d4d49c" />

6. This is ***MOST*** crucial part. We need to *all* have the same .NET version, in this case just select .NET 10, if it doesn't exist, you can download it.
   
   <img width="580" height="209" alt="{E1FBD6F4-9265-48AF-AABE-3AAF4F3541C6}" src="https://github.com/user-attachments/assets/aa8d2f4f-af83-473c-b1a2-f2ad1211c63a" />

# 4.1 Downloading .NET 10

### If by any chance you downloaded Visual Studio 2026, you can safely disregard the guide below for downloading .NET 10.

In case you do not have .NET 10 installed, follow this section. 
> at most cases, if you have downloaded Visual Studio 2026, it should grab .NET 10 when you download Windows Forms App, but it can be heavier than 2022, although 2022 seems much worse in my experience.

x.1 Click this [link](https://dotnet.microsoft.com/en-us/download) to proceed on .NET download site. It should look like this:

  <img width="1824" height="907" alt="image" src="https://github.com/user-attachments/assets/e03a863a-44be-4194-a2ed-09a4c11dfabd" />

x.2 Click the download button, and it should start immediately then wait for it to finish downloading.

x.3 Launch the .exe file, it should look like this:

  <img width="646" height="492" alt="{CEE8EFCC-8F96-4690-8E8C-2609111076C8}" src="https://github.com/user-attachments/assets/613f2000-d5e3-42d9-a128-ec52ab82b56a" />

### Just proceed with the installation, wait for it to finish, relaunch Visual Studio, repeat the 4th step and this should appear now.

5. Proceed with creating the Windows Forms.
   
   <img width="774" height="748" alt="{060013DD-FEF7-431B-B45A-7C288ADD2BBE}" src="https://github.com/user-attachments/assets/d6ac45d6-2972-4a2a-8283-4e722d5249d7" />

6. The initial form will look like this, which basically means that we have successfully created our project file.

   <img width="1220" height="682" alt="{033BE3A2-2969-43DF-9E6A-69677D4BFD20}" src="https://github.com/user-attachments/assets/8322ef40-08a4-42e4-ad60-9f1f257a91ce" />

6.1 You can look at the Solution Explorer by clicking on View > Solution Explorer, or perform a keybind by doing Ctrl + Alt + L

6.2 The solution explorer would look like this. As I have mentioned earlier, we need to rename these files to ensure transparency.
  
  <img width="299" height="207" alt="{E10E8146-FD3C-422B-9615-0ED0E2BC0D7D}" src="https://github.com/user-attachments/assets/84f46058-8f73-4026-bb4a-a47bbcfd265a" />

6.3 To do so, left-click once on the form (in this case, it's the Form1.cs") then right-click, and then rename.
  
  <img width="387" height="446" alt="{FE33B8B0-AF7C-4B91-B0D9-F09AE54F71A5}" src="https://github.com/user-attachments/assets/1f454540-90f1-4cf3-8aa6-68d15da87536" />

6.4 Rename and then hit enter, if you have a prompt that asks if should it change everything onto that name, hit yes so that it will automatically rename itself to it.

6.5 With it being renamed, it would look like this now, but notice there's still that one "Form1" to our project. You can safely ignore this as it won't do any errors. If skeptical, run your first test to see the blank window executed.
  
  <img width="300" height="205" alt="{C69F2733-0927-4E1A-8B41-48669000DB28}" src="https://github.com/user-attachments/assets/4d9241b8-84dc-44d4-9dd2-97181b0afdf7" />

6.6 A quick recap - to access the location of the project folder/s, it's at `C:\Users\<your-username>\source\repos`, and you'll find the name of your project. Since earlier we decided to make different names for project and solution, you'll have two folders inside of the `repos` directory.
  
  ### Solution Folder
  
  <img width="249" height="127" alt="{B82F21DD-E3A9-4D30-8991-0E2B70ABE75B}" src="https://github.com/user-attachments/assets/17d572e5-c752-43a6-8387-453a5753aba7" />

  ### Project Folder
  
  <img width="805" height="324" alt="{04EFEC94-B028-4EBE-8FA9-EFCB718627D1}" src="https://github.com/user-attachments/assets/b9f5f3fd-0e99-4fb4-bda3-6fbed8b93617" />

# six-seven Installing GunaUI

7. To install Guna, go to Tools > NuGet Package Manager > Manage NuGet packages for Solution

   <img width="566" height="274" alt="{C4C0A4EC-25B7-4787-923C-A4930C31DBB5}" src="https://github.com/user-attachments/assets/22427000-8cce-4116-a63b-4d43a1494b2e" />

7.1 On top-right side, check if the package source is using `nuget.org`, since this is where Guna Framework can be downloaded.
  
  <img width="280" height="79" alt="{F3D099CA-8339-4F69-A458-B1E713EB4E9F}" src="https://github.com/user-attachments/assets/8bb66fd2-03fe-444f-974f-2ea60c62d8b3" />

7.2 Click on "Browse", click the search bar and type "Guna", it should appear as the first thing in the search result.
  
  <img width="755" height="172" alt="{C44A31FD-E1A2-44AB-B469-226E4C30AD80}" src="https://github.com/user-attachments/assets/64d49925-1beb-403f-b67c-c179f52a5551" />

7.3 Click the project name, then "Install", there will be a prompt about License, just click "Accept" to proceed on installation.
  
  <img width="631" height="360" alt="{BB31593C-E34B-47A8-8118-9419D0E97EED}" src="https://github.com/user-attachments/assets/de795550-1264-48b1-abd8-b9913b42c6ce" />

7.4 Go back to your designer, if toolbox is not present, click on View > Toolbox, or perform a keybind by doing Ctrl + Alt + X, and Guna components should now appear.
  
  <img width="1206" height="537" alt="{1CB803D8-FB80-436C-A2AB-52D3FDCBBE42}" src="https://github.com/user-attachments/assets/77827cd5-cb6e-4e56-a200-c94ae9fa7eae" />

> This is where you'll handle most things by yourself, do some researching on the components, some can be useful especially for what your assigned task is.

# Performing Git

1. Before you can commit and push files, you need to be a contibutor first, then clone the repository and proceed with the changes.

1.1 To clone a repository, click on Git > Clone Repository, and paste this repository: https://github.com/jgz365/pl-project
  
  <img width="481" height="131" alt="{9E647163-0552-4F87-B944-3EF23794E268}" src="https://github.com/user-attachments/assets/d8010c40-f366-4e36-b2c2-15f04d049564" />

### Click on clone, and it should switch onto your cloned repository.

1.2 To check if you're in the repository, look at the bottom and you should see these:
  
  <img width="359" height="114" alt="{60E1AF89-04EB-4E6B-802F-B6C7587745A0}" src="https://github.com/user-attachments/assets/15737899-e11c-45b8-9074-7f32db8261b4" />

1.3 This is separate from the project that you have created earlier, to check, locate the `repos` folder, and you should see that the project folder and the cloned repository folder is not in one folder structure.

  <img width="170" height="109" alt="{7EAD8677-DC9E-46EF-BF81-A6B341596C77}" src="https://github.com/user-attachments/assets/bee4ec87-cc96-4c4c-9d81-7e9089c3350a" />

# X.0 CREATE A BRANCH

Click on Git > New Branch 

  <img width="500" height="239" alt="{0C8E847C-9974-489E-9FD4-CFDF6ECF9D7C}" src="https://github.com/user-attachments/assets/6a0a5086-1a3c-48b5-965a-e135cad7bfb7" />

Then switch to your branch, and then proceed with 1.4

1.4 Create a folder in the Solution Explorer, this is the crucial part to keep things safe and clean.

Right-click on Solution Explorer's tab > Add > New Folder, then add your desired name to it.

<img width="421" height="120" alt="{C79F5FA7-4BBF-4A60-A88C-E6132C116265}" src="https://github.com/user-attachments/assets/9636b3a1-2c37-4547-9d0e-00b9b5725b6b" />

1.5 To copy the contents, go to your file explorer and locate your project folder, copy the contents inside and paste it onto the folder you have created inside of the repository, Visual Studio will reload itself and you'll see the contents of the folder having the pasted items.

  <img width="357" height="189" alt="image" src="https://github.com/user-attachments/assets/17b21948-3b48-4c0b-af9c-a512f85c706a" />

1.6 It is important to copy these two since they will be the components needed to launch your program.  
> you might see a folder named `.vs`, you can safely disregard this one as Visual Studio creates this file itself whenever it builds the program.

  <img width="201" height="156" alt="{730FF496-7FC1-40AB-ABCE-89495B3D96B2}" src="https://github.com/user-attachments/assets/d11f55ef-bda1-47e8-b610-8b134b05930e" />

1.7 If all goes well, you can now perform a commit in the repository to have the files uploaded. Git > Commit or Stash

  <img width="222" height="430" alt="{47E09D12-9895-43FF-B08A-F4F88B27D8F1}" src="https://github.com/user-attachments/assets/3ef0bc2c-c3b2-4bba-a4c7-23f7937e0fb8" />

1.8 It is required to add a commit message, make sure it's a proper commit message to keep things professional.

  <img width="375" height="499" alt="Capture" src="https://github.com/user-attachments/assets/4363e7ae-dd0f-4119-bd4e-cbfec289abaa" />

1.9 Click the up arrow with an underline icon which is "Push", this is what uploads it to the repository.

  <img width="376" height="177" alt="Capture1" src="https://github.com/user-attachments/assets/31a6c95b-f308-4089-9e9c-7580c8bdf393" />

2.0 A message prompt will appear for Pull Request since you're in a branch. Click "Create in browser" and make a Pull Request.

# ⚠️ Don't merge it yourself, ask me before doing so!











