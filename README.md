# DirectorySize
## A simple Windows application for calculating directory size and sorting the results

#### About:
DirectorySize is a lightweight console application that is intended to quickly calculate the size of all subfolders of the provided folder and the contained files. It can be manipulated
to sort by folder/file (showing folders first) or unsorted - sorting based on user choice, or by default - by size, descending. Currently the app support only sort by size.

#### Usage:
In order to effortlessly use the app - you need to download it in one of the following folders:
C:\Windows
C:\Windows\System32

Then you need to open a command prompt (start -> run -> cmd) or (winlogo + r) -> cmd. You need to either provide the full path of the folder you need to check, or navigate to it. 
The possible parameters are:
/s              Sort by size (ascending)
/S              Sort by size (descending)
/u              Ungroupped - Files and directories will be mixed
/?              Display the help

#### Examples:
```
ds D:\
```
This will list all directories in drive D:, by default the program will display groupped, sorted descending

```
ds D:\Games\ /u
```
This will display the contained subfolders of D:\Games\ sorting by size descending and mixing files and folders

```
ds C:\Users\ /s
```
This will display the contents of C:\Users sorting by size ascending, groupping (directories first, then files).


#### Notes:
This is a software written by an amateur in his spare time, so have no huge expectations. Currently, due to Windows limitations*, some of the files can not be read by this app, thus the size displayed, might slight vary
from its actual size. During tests - the differences were not quite significant, but still it has to be kept in mind. The very idea I started working on this was to have some way of sorting all directories, 
without having to open the properties in each of it.
________________________________________________________________
* PathTooLong - when path exceeds 260 characters the program cannot read it. UnauthorizedPermission - when the program has no permissions to read certain system/temporary files

#### Plans
* Fixing bugs that will come out, 
* working around the Windows limitations to accurate size, creating GUI.
* create installer