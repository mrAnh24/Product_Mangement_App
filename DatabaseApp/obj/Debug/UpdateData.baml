   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	       DatabaseApp.UpdateData   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � 
ResizeMode$  NoResize=�5   1    � WindowStartupLocation$ CenterScreen=�5       � Title$ 
ExcelToSql��5	   	   $	��500��6   $	��500��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��40 �6   �� 5      �� 5      $��90 �6   �� 5      $��50 �6   .+�5   
   � $��0��6   .+�5      +   %DatabaseApp.View.UserControls.MenuBar  5      � 5   
   $��1��6   
�����F�$��	10,0,10,0q�63   .+�5      �� -   5        $��lblTitle��5      $��Data management��6$   
�������$��25(�6V   $��Center=�5      $��Center=�61   $��	0,0,0,-10q�6L   �� 5      -   5        $��btnClear��5      $
��🔄��6%   $
��auto��62   $
��auto��6@    � Cursor$
 Hand��6M   $��Right=�5      $��0,0,20,0q�61   
������ $��25(�5   .   5      � 5   
   $��2��6   
�����F�$��	10,0,10,0q�63   .+�5       &   System.Windows.Controls.DataGrid  -   5        $��dgData��5       $��
15,10,15,0q�6%      AutoGenerateColumns .    AlternatingRowBackground
 ����� �� AlternationCount$ 2��5"      5#      5$      � 5&   
   $��3��6   
�����F�$��	10,0,10,0q�63    � ColumnDefinitions 5'      �� 5(      $	��105 �6#   �� 5)      �� 5*      $��60 �6#   .+�5-      � $��0��6   $��	15,0,10,0q�6#   .+�5.      �� $��Center=�6   .+�5/      �� $��
Data Type:��6    
�������$��18(�6J   $��0,0,0,5q�6X   �� 50      $��
File name:��6    
�������$��18(�6J   $��0,0,0,5q�6X   �� 51      $��Sheet:��6    
�������$��18(�6F   $��0,0,0,5q�6T   52      53      � 55      $��1��6   $��	10,0,15,0q�6#   .+�56      �� $��Center=�6   .+�57      �� -   5        $��cbType��57      $
��auto��6-   $��-8,0,0,8q�6;   	 �� 
IsReadOnly	 . $
��Left=�58      
���   �$
��auto��59   2   .@�5:      �� -   5        $��cbCustomers��5:   '   $��	Customers��6:   �� 5;      -   5        $��	cbProduct��5;   '   $��Products��68   �� 5<      -   5        $��	cbAccount��5<   '   $��Accounts��68   �� 5=      -   5        $��	cbSpecial��5=   '   $��Test Functions��68   {�.  $y�	Collapsed=�5>   '   
��� ���6>   �� 5?      -	   5        $��cbAdvanceCustomers��5?   '   $��AdvanceCustomers��6A   $y�	Collapsed=�6\   �� 5@      -
   5        $��	cbInvoice��5@   '   $��CustomerInvoice��68   $y�	Collapsed=�6R   5A      �� 5B      -   5        $��tbFile��5B      $��20��6-   $��	-8,0,-8,8q�69   {�.  
���   �6^   �� 5C      -   5        $��cbSheet��5C      $��18��6.   $��-8,0,0,0q�6:   
 �� IsTextSearchEnabled
 .  $
��Left=�5D      
���   �$
��auto��5E   2   5F      5G      � 5I      $��2��6   $��	10,0,15,0q�6#   .+�5J      �� -   5        $��	btnUpload��5J      $
��📁��6*   $��20��67   $��	-10,4,0,0q�6C   $
 Hand��5K   1   5L      5N      � 5P   
   $��4��6   
�����F�$��
10,0,10,10q�63   .+�5Q      �� -   5        $��	btnImport��5Q      $
��📤��6&   $��30��63   $
 Hand��6?   $
��Left=�5R      $��	40,0,0,10q�60   6C   �� 5S      -   5        $��	btnUpdate��5S      $��Update��6&   $��30��67   $
 Hand��6C   $
��Left=�5T      $��
150,0,0,10q�60   6D   �� 5U      -   5        $��	btnExport��5U      $
��📥��6&   $��30��63   $
 Hand��6?   $��Right=�5V      $��
0,0,150,10q�61   6E   �� 5W      -   5        $��	btnDelete��5W      $��Delete��6&   $��30��67   $
 Hand��6C   $��Right=�5X      $��	0,0,40,10q�61   6D   5Y      5[      5\      