   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	   #    DatabaseApp.AccountManagement   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � 
ResizeMode$  NoResize=�5   1    � WindowStartupLocation$ CenterScreen=�5       � Title$ AccountManagement��5	   	   $	��500��6#   $	��600��60   .��5
      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��40 �6   �� 5      �� 5      $��50 �6   �� 5      $��15 �6   .+�5   
   � $��0��6   .+�5      +   %DatabaseApp.View.UserControls.MenuBar  5      � 5   
   $��1��6   
�����F�$��	15,0,15,0q�63    � ColumnDefinitions 5      �� 5      $
��3.6* �6#   �� 5      .+�5      � $��0��6   .+�5      �� -   5        $��tbsearch��5      $��Center=�6*   $��10,0,0,0q�6E   $��25��5      $��18(�6&   �� 5       -   5         ��Name$ btnClear��5       $��80��6+   $��Right=�66   $��X��6R   
�������
������ $��0q�5!   I   $��0,9,30,9q�5"   0    � Cursor$
 Hand��6C   �� 5#      -   5        $��cbSearchbox��5#      $��Right=�6.   $
��auto��6J    �� 
IsEditable .  
���   �.@�5%      �� $��Email��6#   �� 5&      $��Username��6#   �� 5'      $
��Role��6#   �� 5(      $��Gender��6#   5)      5*      � 5,      $��1��6   .+�5-      �� -   5        $��	btnSearch��5-      $��Search��6*   $��10,0,0,0q�6<   $
��Left=�5.      $
 Hand��6L   �� 5/      -   5        $��	btnReload��5/      $
��🔄��6*   $��0,0,10,0q�67   $��20(�6I   $��Right=�50      $
 Hand��6M   $
��auto��51      $
��auto��6&   
������ 64   52      53      � 55   
   $��2��6   
�����F�$��	15,0,15,0q�63    56      �� 57      $
��269* �6#   �� 58      $	��16* �6#   .+�5:      &   System.Windows.Controls.DataGrid  -   5        $��	dgAccount��5:      $��10,10,10,10q�6(   $��2��6=      AlternatingRowBackground
 �����	 �� AlternationCount$	 2��5;   <   
   
IsReadOnly
 .    SelectionMode$ Single=�5<   )      CanUserResizeRows .   � 	Resources 5=      )  #System.Windows.Controls.DataGridRow �� 
TargetType��%E   '        �� 5>      "  6   .�5?      -   5        5@      5A      5B      �� 5C      -   5        $��lblError��5C      $7��1Reload before choosing any account to avoid error��6(   
���  ��$��	Underline{�5D      $��	10,-8,0,0q�64   $y�Hidden=�6G   $��2��6[   5E      � 5G   
   $��3��6   
�����F�$��	15,0,15,0q�63    5H      �� 5I      �� 5J      .+�5M      � $��0��6   $��10,0,0,0q�6#   $��Center=�65   .+�5N      �� -	   5        $��txtTotal��5N      $��Total accounts: ...��6,   $��18(�6G   
�������5O      5P      � 5R      $��1��6   .+�5S      �� -
   5        $��btnAdd��5S      $	��Add��6'   $��10,0,0,0q�65   $
��Left=�5T      $
 Hand��6I   �� 5U      -   5        $��	btnUpdate��5U      $��Update��6*   $��0,0,0,0q�6;   $
 Hand��5V   1   �� 5W      -   5        $��	btnDelete��5W      $��Delete��6*   $��0,0,10,0q�6;   $��Right=�5X      $
 Hand��6M   5Y      5Z      5[      5\      