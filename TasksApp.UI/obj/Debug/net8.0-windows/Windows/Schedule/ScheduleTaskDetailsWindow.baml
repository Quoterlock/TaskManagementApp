   M S B A M L   `   `   `  ����F  BTasksApp.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:TasksApp.UI.Windows.ScheduleTasksApp.UI.Windows.Schedule  5   	   <    6TasksApp.UI.Windows.Schedule.ScheduleTaskDetailsWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:TasksApp.UI.Windows.Schedule     5   	     � Title$  TaskDetailsWindow��5      $	��200��6   $	��300��6,   .��5	      �  � RowDefinitions 5
   
   �� 5      $��24 �6   �� 5      $��48 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��* �6   �� 5      $��24 �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��2��6   .��6$   	Task text6/   �� 5   
   -   5         ��Name$ taskTextBox��5   3   $��1��6   $��2��6   6H   �� 5   
   $��2��6   .��6   
Start Time6)   �� 5   
   -   5        $ startTimeTextBox��5      $��3��6   69   �� 5   
   $��2��6   $��1��6   .��6-   
End Time67   �� 5   
   -   5        $ endTimeTextBox��5   /   $��3��6   $��1��6   6G   �� 5    
    �� Orientation$ 
Horizontal=�6   $��7��6.   $��1��6;   $��Right=�6K   .+�5!      �� -   5         ��Name$ saveBtn��5!      $��60��6<   .��6G   Save6M   �� 5"      -   5        $ 	deleteBtn��5"      $��60��6(   .��6K   Delete6S   5#      5$      5%      