   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	        DatabaseApp.PasswordChange   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   -   5          � WindowStyle$
  None=�5       � WindowStartupLocation$ CenterOwner=�6-    � 
ResizeMode$ NoResize=�5   ;    � Title$ PasswordChange��5	   	   $	��250��6    $	��400��6-   .��5
      � 
�����F�$��5q�6"    � RowDefinitions 5   
   �� 5      $
��0.8* �6   �� 5      $��3* �6   �� 5      $
��0.8* �6   .+�5   
   � $��0��6   $��Center=�6   $��Center=�67   .+�5      �� -   5        $��txtName��5      $��Change password for ...��6'   $��20(�6F   5      � 5   
   $��1��6   $��	40,0,40,0q�6   .+�5      �� .+�5      �� $�� Old Password��6   $��13(�60   �� 5      -   5        $��tbOldPassword��5      $��15(�6/   $��18��6=   $��8,0,8,0q�6I   G� 5      -   5        $��pbOldPassword��5      $��15(�63   $��18��6A   $��	8,-18,8,0q�6M   �� 5      $�� New Password��6   $��13(�60   �� 5      -   5        $��tbNewPassword��5      $��15(�6/   $��18��6=   $��8,0,8,0q�6I   G� 5      -   5        $��pbNewPassword��5      $��15(�63   $��18��6A   $��	8,-18,8,0q�6M   �� 5      $�� Confirm new Password��6   $��13(�68   �� 5      -   5        $��tbConfirmedPassword��5      $��15(�65   $��18��6C   $��8,0,8,0q�6O   G� 5      -   5        $��pbConfirmedPassword��5      $��15(�69   $��18��6G   $��	8,-18,8,0q�6S   �� 5       -	   5        $��
passwordCb��5       $��Show password��6-   $��13(�6E   $��7,3,0,0q�6S   5!      5"      5#      � 5%   
   $��2��6   $��	90,0,90,0q�6   .+�5&      �� -
   5        $��
btnConfirm��5&      $��Confirm��6'   $
��Left=�69    � Cursor$
 Hand��5'   .   �� 5(      -   5        $��	btnCancel��5(      $��Cancel��6&   $��Right=�67   $
 Hand��5)   -   5*      5+      5,      