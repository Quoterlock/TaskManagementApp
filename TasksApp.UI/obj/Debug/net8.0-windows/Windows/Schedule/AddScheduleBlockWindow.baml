   M S B A M L   `   `   `  ����F  BTasksApp.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:TasksApp.UI.Windows.ScheduleTasksApp.UI.Windows.Schedule  5   	   9    3TasksApp.UI.Windows.Schedule.AddScheduleBlockWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:TasksApp.UI.Windows.Schedule     5   	     � Title$  AddSheduleBlockWindow��5   	   $	��250��6'   $	��400��64   .��5	      �  � RowDefinitions 5
   
   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��* �6   �� 5      $��24 �6   .+�5   
   �� $��Title��6   �� 5   
   -   5         ��Name$ titleTextBox��5      $��1��6(   �� 5   
   $��Duration (time)��6   $��2��6*   �� 5   
    �� Orientation$ 
Horizontal=�6   $��3��6.   .+�5      �� $��start��6   �� 5      -   5        $ startTimeTextBox��5      $	��100��60   �� 5      $��- end��6   �� 5      -   5        $ endTimeTextBox��5      $	��100��6.   5      �� 5   
   $��Repeat every:��6   $��4��6(   �� 5   
   $ 
Horizontal=�6   $��5��6.   .+�5      �� -   5         ��Name$ 
moCheckBox��5   $   $��Mo��6   6$   �� 5       -   5        $ 
tuCheckBox��5    $   $��Tu��6   6$   �� 5!      -   5        $ 
weCheckBox��5!   $   $��We��6   6$   �� 5"      -   5        $ 
thCheckBox��5"   $   $��Th��6   6$   �� 5#      -   5        $ 
frCheckBox��5#   $   $��Fr��6   6$   �� 5$      -	   5        $ 
saCheckBox��5$   $   $��Sa��6   6$   �� 5%      -
   5        $ 
suCheckBox��5%   $   $��Su��6   6$   5&      �� 5'   
   $ 
Horizontal=�6   $��7��6.   $��Right=�6;   .+�5(      �� -   5         ��Name$ addBtn��5(      $	��Add��6%   $��60��63   6>   �� 5)      -   5        $ 	cancelBtn��5)      $��Cancel��6(   $��60��69   6D   5*      5,      5-      