   M S B A M L   `   `   `  ����F  BTasksApp.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:TasksApp.UI.WindowsTasksApp.UI.Windows  5   	   '    !TasksApp.UI.Windows.NewTaskWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:TasksApp.UI.Windows     5   	     � Title$  NewTaskWindow��5   	   $	��250��6   $	��400��6,   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��* �6   �� 5      $��24 �6   .+�5   
   �� .��6   	Task text6   �� 5   
   -   5         ��Name$ projectsComboBox��5      $��1��6-   �� 5   
   -   5         ��Name$ taskTextBox��5   3   $��1��6   $��2��6   63   �� 5!   
   -   5         ��Name$ scheduledCheckBox��5!   4   $��2��6   $��2��6    $��	Scheduled��6O    d� 	IsChecked$ FalseY�6c   6�   �� 5#   
   $��3��6   $��2��6   .��62   DueTo (Date)6@   (  "System.Windows.Controls.DatePicker  5$   
   -   5          Name$ dueToDatePicker��5$   H   $��4��6   $��2��6"   ${�False��66   6H   �� 5%   
   -   5        $ timeBlockedCheckBox��5%   F   $��5��6   $��2��6    ${�False��64   $��Time blocked��6c   $ FalseY�6z   6�   �� 5'   
   $��6��6   .��6   
Start Time6)   �� 5(   
   -   5        $ startTimeComboBox��5(      $��7��6.   ${�False��6;    �� 
IsEditable$
 True��6M   �� 5*   
   $��6��6   $��1��6   .��6-   
End Time67   �� 5+   
   -   5        $ endTimeComboBox��5+      $��7��6,   $��1��69   ${�False��6I   $
 True��6[   �� 5-   
   $��9��6   $��2��6"   $��Right=�66   	 �� Orientation$	 
Horizontal=�6R   .+�5.      �� -   5        
 ��Name$
 	createBtn��5.       $��60��6   .��6K   Create6S   �� 5/      -	   5        $
 	cancelBtn��5/       $��60��6   .��6K   Cancel6S   50      51      52      