   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   "    DatabaseApp.UpdateProductAdd   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	     � WindowStyle$
  None=�5       � WindowStartupLocation$ CenterOwner=�6-    � 
ResizeMode$ NoResize=�5   	    � Title$ Product Add��5	   	   $	��360��6   $	��400��6*   .��5
      � 
�����F� � RowDefinitions 5   
   �� 5      $��0.25* �6   �� 5      �� 5      $
��0.2* �6   .+�5   
   � $��0��6   .+�5      �� $��Add new product��6   $��Center=�6/   $��Center=�6L   $��30(�5      $��DemiBold"�6&   5      � 5   
   $��1��6   $��	60,0,60,0q�6    5      �� 5      �� 5      $��0.65* �6    .+�5      � $��0��6   .+�5      �� .+�5      �� $'��!Product ID * (Ex format: 0000-AA)��6    $��14(�6I   $��0,0,0,5q�5       �� 5       -   5        $��txtProductID��5       $��14(�62   $��0,0,0,5q�6@    �� 	MaxLength$ 7��6Q   �� 5!      $��Product name *��6    $��14(�66   $��0,0,0,5q�6D   �� 5"      -   5        $��
txtProduct��5"      $��14(�60   $��0,0,0,5q�6>   �� 5#      $��Description��6    $��14(�63   $��0,0,0,5q�6A   �� 5$      -   5        $��txtDescription��5$      $��14(�64   $��0,0,0,5q�6B   5%      5&      � 5(      $��1��6    � ColumnDefinitions 5)      �� 5*      �� 5+      .+�5.      � $��0��6   $��0,0,20,0q�6'   .+�5/      �� .+�50      �� $��Price *��6$   $��14(�63   $��0,0,0,5q�6A   �� 51      -   5        $��txtPrice��51   "   $��14(�62   $��0,0,0,5q�6@   $ 10��52   "   61   �� 53      $��Amount *��6$   $��14(�64   $��0,0,0,5q�6B   �� 54      -   5        $��	txtAmount��54   "   $��14(�63   $��0,0,0,5q�6A   $ 4��55   "   60   56      57      � 59      $��1��6   $��20,0,0,0q�6'   .+�5:      �� .+�5;      �� $��Type *��6$   $��14(�62   $��0,0,0,5q�6@   �� 5<      -   5        $��cbType��5<   #   $��14(�61   $��0,0,0,5q�6?   $
��auto��6P   $��21��6]   
���   � �� 
IsEditable .  .@�5>      �� $
��Meat��6+   �� 5?      $��Dairy��6+   �� 5@      $��	Vegetable��6+   �� 5A      $��Drink��6+   �� 5B      $��Fruit��6+   �� 5C      $��Dessert��6+   �� 5D      $��Snack��6+   �� 5E      $��Other��6+   5F      �� 5G      $��Status��6$   $��14(�62   $��0,0,0,5q�6@   �� 5H      -   5        $��	txtStatus��5H   "   $��14(�63   $��0,0,0,5q�6A   $
��auto��5I   "   {�.  
���   �6A   5J      �� 5L      -   5        $��btnFill��5L      $
��🎲��6,   $��Bottom=�69    � Cursor$
 Hand��6T   $��Right=�5M      $��	0,0,-50,5q�6P   $��30��5N      
����  �6(   5O      5P      5Q      � 5S   
   $��2��6    5T      �� 5U      �� 5V      �� 5W      .+�5Z      � $��0��6   .+�5[      �� -	   5        $��
btnConfirm��5[      $��Confirm��6+   $��Right=�6=   $
 Hand��6Y   5\      5]      � 5_      $��1��6   .+�5`      �� -
   5        $��btnClear��5`      $��Clear��6)   $
 Hand��69   5a      5b      � 5d      $��2��6   .+�5e      �� -   5        $��btnBack��5e      $
��Back��6(   $
��Left=�67   $
 Hand��6R   5f      5g      5h      5i      5j      