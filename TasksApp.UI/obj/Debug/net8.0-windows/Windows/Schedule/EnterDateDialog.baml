   M S B A M L   `   `   `  ����F  BTasksApp.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:TasksApp.UI.Windows.ScheduleTasksApp.UI.Windows.Schedule  5   	   2    ,TasksApp.UI.Windows.Schedule.EnterDateDialog   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:TasksApp.UI.Windows.Schedule     5   	     � Title$  EnterDateDialog��5   	   $	��150��6!   $	��200��6.   .��5	      �  � RowDefinitions 5
   
   �� 5      $��34 �6   �� 5      $��24 �6   �� 5      $��* �6   �� 5      $��24 �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ messageTextBlock��5      $��2��6.   $
��Wrap=�6B    �� TextAlignment$ Center=�6V   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ 
datePicker��5   6   $��1��6   $��2��6"   66   �� 5   
   -   5         ��Name$ 
confirmBtn��5      $��4��6%   .��6K   	Confirm6T   �� 5   
   -   5        $ 	cancelBtn��5      $��4��6$   $��1��61   .��6A   Cancel6I   5      5      