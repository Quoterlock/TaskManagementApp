   M S B A M L   `   `   `  ���� F  BTasksApp.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:TasksApp.UI.Windows.ScheduleTasksApp.UI.Windows.Schedule  :    4TasksApp.UI.Windows.Schedule.EditScheduleBlockWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     <local*clr-namespace:TasksApp.UI.Windows.Schedule       � Title$  
Edit block��$	��250��$	��400��.���  � RowDefinitions �� $��24 ��� $��24 ��� $��24 ��� $��24 ��� $��24 ��� $��24 ��� $��* ��� $��24 �.+��� $��Title���� -    ��Name$ titleTextBox��$��1���� $��Duration (time)��$��2����  �� Orientation$ 
Horizontal=�$��3��.+��� $��start���� -   $ startTimeTextBox��$	��100���� $��- end���� -   $ endTimeTextBox��$	��100���� $��Repeat every:��$��4���� $ 
Horizontal=�$��5��.+��� -    ��Name$ 
moCheckBox��$��Mo���� -   $ 
tuCheckBox��$��Tu���� -   $ 
weCheckBox��$��We���� -   $ 
thCheckBox��$��Th���� -   $ 
frCheckBox��$��Fr���� -	   $ 
saCheckBox��$��Sa���� -
   $ 
suCheckBox��$��Su���� $ 
Horizontal=�$��7��$��Right=�.+��� -    ��Name$ saveBtn��$
��Save��$��60���� -   $ 	cancelBtn��$��Cancel��$��60��