   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   '    !DatabaseApp.ProductListUserAmount   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   -   5          � 
ResizeMode$  NoResize=�5   1    � WindowStyle$
 None=�5       � WindowStartupLocation$ CenterOwner=�6-    � Title$ PasswordConfirmation��5   	   $	��120��6&   $	��300��63   .��5	      � 
�����F�$��5q�6"    � RowDefinitions 5
   
   �� 5      $
��0.4* �6   �� 5      $��0.55* �6   �� 5      .+�5   
   � $��0��6   $��Right=�6   $��Center=�68   .+�5      �� -   5        $��X��5      $��16(�6    $��Microsoft Tai Le)�6.   $��0,-5,5,0q�6L   $��Right=�5       � Cursor$
 Hand��5      5      � 5   
   $��1��6   $��Center=�6   $��Center=�69   .+�5      �� -   5        $��txtName��5      $��Enter amount of ...��6'   
�������$��20(�5      $��Microsoft Tai Le)�6"   5      � 5   
   $��2��6   $��Center=�6   $��	40,0,40,0q�67   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5        $��tbSubmit��5      $	��140��6*   $
��Left=�66   $��16(�6Q    �� 	MaxLength$ 2��5   G   �� 5      -   5        $��	btnSubmit��5      $��Right=�6*   $��confirm��6F   $
 Hand��5    1   5!      5"      5#      5$      