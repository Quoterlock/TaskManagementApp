   M S B A M L   `   `   `  ����F  BTasksApp.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:TasksApp.UI.PagesTasksApp.UI.Pages  5      $    TasksApp.UI.Pages.SchedulePage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:TasksApp.UI.Pages     5        N� Title$  SchedulePage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��* �6   �� 5      $��24 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��4��6.   .+�5      �� -   5         ��Name$ 
addItemBtn��5      $��Add Item��6)   $��60��6<   6G   �� 5      -   5        $ clearBtn��5      $��	Clear All��6'   $��60��6;   6F   5      �� 5   
   $��1��6   .��6   Monday6%   �� 5    
   -   5         ��Name$ mondayListBox��5       $��2��6   6   �� 5!   
   $��1��6   $��1��6   .��6-   	Tuesday66   �� 5"   
   -   5        $ tuesdayListBox��5"   /   $��2��6   $��1��6   6/   �� 5#   
   $��1��6   $��2��6   .��6-   	Wednesday68   �� 5$   
   -   5        $ wednesdayListBox��5$   /   $��2��6   $��2��6   6/   �� 5%   
   $��1��6   $��3��6   .��6-   
Thursday67   �� 5&   
   -   5        $ thursdayListBox��5&   /   $��2��6   $��3��6   6/   �� 5'   
   $��3��6   $��0��6   .��6-   Friday65   �� 5(   
   -   5        $ fridayListBox��5(   /   $��4��6   $��0��6   6/   �� 5)   
   $��3��6   $��1��6   .��6-   
Saturday67   �� 5*   
   -   5        $ saturdayListBox��5*   /   $��4��6   $��1��6   6/   �� 5+   
   $��3��6   $��2��6   .��6-   Sunday65   �� 5,   
   -	   5        $ sundayListBox��5,   /   $��4��6   $��2��6   6/   5.      5/      