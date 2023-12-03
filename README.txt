In this repository there are two .ipynb (Jupyter Notebook) files. And one "EyeTracker VSHD.sln" file. For the EyeTracker VSHD.sln file to work you need to put your ViewPoint license file here: EyeTracker VSHD\EyeTracker VSHD\bin\Debug\net6.0\License.

"Precision and accuracy single file.ipynb" is used to find precision and accuracy of a single data file made using the "Eyetracker VSHD.sln" program. 
To run it just set the file directory and eye in the 2nd cell of code, then run the whole notebook. 
This file is split into more cells and is easier to change than the "Precision and accuracy multiple file.ipynb", it is useful for debugging/analyzing a single file.

"Precision and accuracy multiple file.ipynb" is used to analyze multiple files at once, it puts them all into a dataframe where precision and accuracy are columns.
Here most of the code is in one cell (to be able to loop through files), and is therefore good if you want to compare different files and if you know the data is relatively solid.
To run this, pick directory in the 2nd cell of code and run the whole notebook.

"EyeTracker VSHD.sln" is used to do the measuring.
To use it properly:
1. Open ViewPoint
2. Adjust VSHD and the settings in viewpoint until everything looks good
3. Run calibration on ViewPoint, make sure the subject of measure lies completely still. 
4. Run the program and set a file name (The program seems to crash if the file name has too many "_" or if its too long)
5. 16 red dots will be shown for 3 seconds each, make the test subject stare as best as possible at them as they pop up.
6. A datafile will be generated, which can then be analyzed using the .ipynb programs.
