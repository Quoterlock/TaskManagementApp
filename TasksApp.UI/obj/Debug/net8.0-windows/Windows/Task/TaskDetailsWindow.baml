   M S B A M L   `   `   `  ����F  BTasksApp.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:TasksApp.UI.WindowsTasksApp.UI.Windows  5   	   +    %TasksApp.UI.Windows.TaskDetailsWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:TasksApp.UI.Windows     5   	     � Title$  TaskDetailsWindow��5   	   $	��250��6#   $	��400��60   .��5	      �  � RowDefinitions 5
   
   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��* �6   �� 5      $��24 �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� .��6   	Task text6   �� 5   
   -   5         ��Name$ projectsComboBox��5      $��1��6-   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $��1��6.   $��2��6;   .+�5       �� -   5         ��Name$ markDoneBtn��5       6*   �� 5!      -   5         ��Name$ taskTextBox��5!      $	��350��6E   5"      �� 5$   
   -   5         ��Name$ scheduledCheckBox��5$   4   $��2��6   $��2��6    $��	Scheduled��6O    d� 	IsChecked$ FalseY�6c   6�   �� 5&   
   $��3��6   $��2��6   .��62   DueTo (Date)6@   (  "System.Windows.Controls.DatePicker  5(   
   -   5        	  Name$	 dueToDatePicker��5(      $��4��6.   $��2��6;   6�   �� 5*   
   -   5        $ timeBlockedCheckBox��5*   F   $��5��6   $��2��6    ${�False��64   $��Time blocked��6c   $ FalseY�6z   6�   �� 5,   
   $��6��6   .��6   
Start Time6)   �� 5-   
   -   5        $ startTimeComboBox��5-      
 �� 
IsEditable$

 True��6.   $��7��6@   6M   �� 5/   
   $��6��6   $��1��6   .��6-   
End Time67   �� 50   
   -   5        $ endTimeComboBox��50      $

 True��6,   $��7��6>   $��1��6K   6[   �� 53   
   $ 
Horizontal=�6   $��9��6.   $��1��6;   $��Right=�6K   .+�54      �� -	   5        $ saveBtn��54      $��60��6<   .��6G   Save6M   �� 55      -
   5        $ 	deleteBtn��55      $��24��6(   .��56      ��    
deleteIcon#���   6   57      58      59      5:      