   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	       DatabaseApp.UpdateData   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � 
ResizeMode$  NoResize=�5   1    � WindowStartupLocation$ CenterScreen=�5       � Title$ Update Data��5	   	   $	��500��6   $	��500��6*   .��5
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
�������$��18(�6F   $��0,0,0,5q�6T   52      53      � 55      $��1��6   $��	10,0,15,0q�6#   .+�56      �� -   5        $��tbInfo��56      $!��This Data Type is read only��6*   $��Right=�6M   $��10q�57      
�������$��	Underline{�6@   $y�	Collapsed=�6\   �� 58      $��Center=�6   .+�59      �� -   5        $��cbType��59      $
��auto��6-   $��-8,0,0,8q�6;   	 �� 
IsReadOnly	 . $
��Left=�5:      
���   �$
��auto��5;   2   .@�5<      �� -   5        $��	cbProduct��5<   '   $��Products��68   �� 5=      -   5        $��
cbCustomer��5=   '   $��Customer��69   �� 5>      -   5        $��cbCustomerListFinal��5>   '   $��CustomerListFinal��6B   �� 5?      -	   5        $��	cbSpecial��5?   '   $��Main database:��68   {�.  
��� ���
�������5@   ;   �� 5A      -
   5        $��cbAccountTest��5A   '   $��AccountTest��6<   $y�	Collapsed=�6R   �� 5B      -   5        $��cbAccountLinked��5B   '   $��AccountLinked��6>   $y�	Collapsed=�6V   �� 5C      -   5        $��cbProductList��5C   '   $��ProductList��6<   $y�	Collapsed=�6R   5D      �� 5E      -   5        $��tbFile��5E      $��20��6-   $��	-8,0,-8,8q�69   {�.  
���   �6^   �� 5F      -   5        $��cbSheet��5F      $��18��6.   $��-8,0,0,0q�6:   
 �� IsTextSearchEnabled
 .  $
��Left=�5G      
���   �$
��auto��5H   2   5I      5J      � 5L      $��2��6   $��	10,0,15,0q�6#   .+�5M      �� -   5        $��	btnUpload��5M      $
��📁��6*   $��20��67   $��	-10,4,0,0q�6C   $
 Hand��5N   1   5O      5Q      � 5S   
   $��4��6   
�����F�$��
10,0,10,10q�63   .+�5T      �� -   5        $��	btnImport��5T      $
��📤��6&   $��30��63   $
 Hand��6?   $
��Left=�5U      $��	40,0,0,10q�60   6C   �� 5V      -   5        $��	btnUpdate��5V      $��Update��6&   $��30��67   $
 Hand��6C   $
��Left=�5W      $��
150,0,0,10q�60   6D   �� 5X      -   5        $��	btnExport��5X      $
��📥��6&   $��30��63   $
 Hand��6?   $��Right=�5Y      $��
0,0,150,10q�61   6E   �� 5Z      -   5        $��	btnDelete��5Z      $��Delete��6&   $��30��67   $
 Hand��6C   $��Right=�5[      $��	0,0,40,10q�61   6D   5\      5^      5_      