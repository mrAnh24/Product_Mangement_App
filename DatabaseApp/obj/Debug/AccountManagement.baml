   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	   #    DatabaseApp.AccountManagement   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � WindowStartupLocation$  CenterScreen=�5       � Title$ AccountManagement��5	   	   $	��500��6#   $	��600��60   .��5
      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��40 �6   �� 5      �� 5      $��50 �6   �� 5      $��15 �6   .+�5   
   � $��0��6   .+�5      +   %DatabaseApp.View.UserControls.MenuBar  5      � 5   
   $��1��6   
�����F�$��	15,0,15,0q�63    � ColumnDefinitions 5      �� 5      $��2.35* �6#   �� 5      .+�5      � $��0��6   .+�5      �� -   5        $��tbsearch��5      $��Center=�6*   $��10,0,0,0q�6E   $��25��5      $��18(�6&   5       � 5"      $��1��6   .+�5#      �� -   5        $��	btnSearch��5#      $��Search��6*   $��10,0,0,0q�6<   $
��Left=�5$      64   �� 5%      -   5        $��	btnReload��5%      $��Reload��6*   $��0,0,10,0q�6;   $��Right=�5&      65   5'      5(      � 5*   
   $��2��6   
�����F�$��	15,0,15,0q�63    5+      �� 5,      $	��25* �6#   �� 5-      $
��124* �6#   �� 5.      $	��41* �6#   .+�50      &   System.Windows.Controls.DataGrid  -   5        $��	dgAccount��50      $��10,10,10,10q�6(   $��3��6=      AlternatingRowBackground
 ����� �� AlternationCount$ 2��51   <      
IsReadOnly .    SelectionMode$ Single=�52   )      CanUserResizeRows .  	 � 	Resources	 53      )  #System.Windows.Controls.DataGridRow
 �� 
TargetType��%E   '        �� 54      "
  6   .�55      -   5        56      57      58      59      � 5;   
   $��3��6   
�����F�$��	15,0,15,0q�63    5<      �� 5=      �� 5>      .+�5A      � $��0��6   $��10,0,0,0q�6#   $��Center=�65   .+�5B      �� -   5        $��txtTotal��5B      $��Total accounts: ...��6,   $��18(�6G   
�������5C      5D      � 5F      $��1��6   .+�5G      �� -   5        $��btnAdd��5G      $	��Add��6'   $��10,0,0,0q�65   $
��Left=�5H      64   �� 5I      -   5        $��	btnUpdate��5I      $��Update��6*   $��0,0,0,0q�6;   5J      �� 5K      -	   5        $��	btnDelete��5K      $��Delete��6*   $��0,0,10,0q�6;   $��Right=�5L      65   5M      5N      5O      5P      