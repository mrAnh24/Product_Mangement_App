   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	   #    DatabaseApp.InvoiceManagement   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � 
ResizeMode$  NoResize=�5   1    � WindowStartupLocation$ CenterScreen=�5       � Title$ Invoice Management��5	   	   $	��500��6$   $	��500��61   .��5
      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $
��0.1* �6   �� 5      .+�5   
   � $��0��6   .+�5      +   %DatabaseApp.View.UserControls.MenuBar  $�� 0,0,0,5q�6#   5      � 5   
   $��1��6   $��	10,0,10,0q�6   
�����F� � ColumnDefinitions 5      �� 5      �� 5      $
��0.2* �6#   .+�5      � $��0��6   .+�5      �� -   5        $��tbSearchBox��5      $��16(�6-   $��26��6;   $
��auto��6G   $��10,0,5,0q�6T   �� 5      -   5         ��Name$ btnSearchClear��5      $��10��61   $
��auto��6<   $��Right=�6J   $��X��6f   
�������
������ $��0q�5   E   $��8q�5      6    5       � 5"      $��1��6   .+�5#      �� -   5        $��	btnSearch��5#      $��Search��6*   $��Right=�6;   $��26��6W    � Cursor$
 Hand��5$      $��0,0,10,0q�6'   69   5%      5'      � 5)   
   $��2��6   $��
10,0,10,10q�6   
�����F� 5*      �� 5+      �� 5,      $
��0.5* �6#   .+�5/      � $��0��6   .+�50      &   System.Windows.Controls.DataGrid  -   5        $��	dgInvoice��50         CanUserAddRows .     CanUserDeleteRows .  $��10,10,10,10q�6]   	   AlternatingRowBackground
	 �����
 �� AlternationCount$
 2��51   @      
IsReadOnly . $��2��52   -      SelectionMode$ Single=�6A      AutoGenerateColumns . $   HorizontalScrollBarVisibility$
 Auto=�54      "   VerticalScrollBarVisibility$
 Auto=�6@    � 	Resources 55      )  #System.Windows.Controls.DataGridRow �� 
TargetType��%E   '        �� 56      "  6    .�57      -   5        58      59      5:      5;      � 5=      $��1��6   $��
0,10,10,10q�6#    5>      �� 5?      $��0.11* �6$   �� 5@      $��0.11* �6$   �� 5A      �� 5B      $
��0.2* �6$   .+�5E      � $��0��6   .+�5F      �� -   5        $��	cbFilter1��5F       �� 
IsReadOnly .  �� 
IsEditable .  $
��auto��6U   
���   �$	��Top=�5G   2   .@�5I      �� $��Full Detail��6'   �� 5J      $��Customer��6'   �� 5K      $��Location��6'   �� 5L      $��Other��6'   5M      5N      � 5P      $��1��6   .+�5Q      �� -   5        $��	cbFilter2��5Q       .  .  $
��auto��6U   
���   �$	��Top=�5R   2   {�.  .@�5T      �� -   5        $��cbCustomerID��5T   '   $��
CustomerID��6;   �� 5U      -	   5        $��
cbUsername��5U   '   $��Username��69   �� 5V      -
   5        $��cbInputName��5V   '   $��	InputName��6:   �� 5W      -   5        $��cbGender��5W   '   $��Gender��67   �� 5Y      -   5        $��	cbCountry��5Y   '   $��Country��68   �� 5Z      -   5        $��cbCity��5Z   '   $
��City��65   �� 5[      -   5        $��cbRegion��5[   '   $��Region��67   �� 5\      -   5        $��	cbAddress��5\   '   $��Address��68   �� 5^      -   5        $��cbPostalCode��5^   '   $��Postal code��6;   �� 5_      -   5        $��cbPaymentMethod��5_   '   $��Payment method��6>   �� 5`      -   5        $��cbCouponCode��5`   '   $��Coupon code��6;   �� 5a      -   5        $��cbPaymentStatus��5a   '   $��Payment status��6>   5b      5c      � 5e      $��2��6   
�������.+�5f      �� $
��Auto=�6#   .��5g      �� $��5q�6%   .+�5h      �� $��Number of customer :��6(   �� 5i      -   5        $��txtCustomer��5i   (   
���"�"�6;   �� 5j      $��Number of sold products :��6(   �� 5k      -   5        $��
txtProduct��5k   (   
���"�"�6:   �� 5l      $��Best selling product :��6(   �� 5m      -   5        $��txt1Product��5m   (   
���"�"�6;   �� 5n      $��Income :��6(   �� 5o      -   5        $��	txtIncome��5o   (   
���"�"�69   �� 5p      $��Highest bill :��6(   �� 5q      -   5        $��
txtHighest��5q   (   
���"�"�6:   �� 5r      $��Most profit country :��6(   �� 5s      -   5        $��	txtProfit��5s   (   
���"�"�69   �� 5t      $��Popular payment method :��6(   �� 5u      -   5        $��
txtPayment��5u   (   
���"�"�6:   5v      5w      5x      � 5z      $��3��6   .+�5{      �� -   5        $��
btnPayment��5{      $
��📠��6/   $
��Left=�6<   $
 Hand��5|      $
��auto��6+   $
��auto��69   $��30(�6F   
������ $��Bottom=�5}      �� 5~      -   5        $��	btnUpdate��5~      $
��📝��6.   $��Right=�6;   $
 Hand��5      $
��auto��6+   $
��auto��69   $��30(�6F   
������ $��Bottom=�5�      $��0,0,50,0q�68   �� 5�      -   5        $��btnClear��5�      $
��🔄��6-   $��Right=�6:   $
 Hand��5�      $
��auto��6+   $
��auto��69   $��30(�6F   
������ $��Bottom=�5�      5�      5�      5�      5�      5�      