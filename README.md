# Copy to (Sub) Dir

This program allows you to copy/move a file to a selected directory, with some extra checks (on top of "cp") in the future. 

1. If the file is busy (e.g. still opening in your editor) it will by default retry the operation up to 2 seconds, and abort only after that. (not implemented yet)
1. If the copied file already exists in the target directory, it does byte-by-byte comparison. If the file is different, it copies/moves the file with the suffix `-2`, `-3` and so on. If the file is the same, for the `copy` operation, it doesn't do anything; for the `move` operation, it removes the source but keeps the destination. (copy operation implemented, move operation doesn't do anything upon duplication)

## How to use it

I use it with FastStone Image Viewer.

1. Right click on the image (in full screen mode)
1. Edit with external programs -> Add/remove external program
1. Add CopyToSubdir as an external program. As parameters, use for example:
 * copy "(filename)" lr-export
 * move "(filename)" not-picked

## Use case

My primary use case right now is to organize files edited in lightroom, in camera, and sometimes in ViewNX in one directory so that I can easily compare them side by side, and then to move the non-selected files to another subdirectory. This also works great with lightroom outputting multiple files with suffix `-$i` whenever you export.