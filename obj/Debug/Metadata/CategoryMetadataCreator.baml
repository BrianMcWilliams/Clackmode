   M S B A M L   `   `   `  ����K  GTrackLayeringNFT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TrackLayeringNFTTrackLayeringNFT  5   	   .    (TrackLayeringNFT.CategoryMetadataCreator   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:TrackLayeringNFT     5   	     � Title$  CategoryMetadataCreator��5   	   $	��427��6)   $	��414��66   .��5	      � .+�5
   
   �� -   5         ��Name$ Title��5
      $��Layer :��6   $
��Left=�61   $��	10,10,0,0q�6L   $	��Top=�6_   $��45��6w   $��80��6�   $��22(�6�   �� 5   
   -   5        $ CategoryName��5      $��(category name)��6&   $
��Left=�6@   $��	90,10,0,0q�6[   $	��Top=�6n   $��45��6�   $	��296��6�   $��22(�6�   $��Italic$�6�   �� 5   
   -   5         ��Name$ 
ContentBox��5      $
��Left=�6&   $	��112��6A   $��
220,43,0,0q�6N   $	��Top=�6b   $	��180��6z   �� 5   
   $��Content��6   $
��Left=�6"   $��
166,49,0,0q�6=   $	��Top=�6Q   $��26��6i   $	��129��6u   �� 5   
   -   5        $ ProbabilityLabel��5      $��Probability : ��6*   $
��Left=�6C   $��	10,75,0,0q�6^   $	��Top=�6q   �� 5   
   -   5         ��Name$ ProbabilityTextBox��5      $
��Left=�6.   $��22��6I   $��	90,79,0,0q�6U    �� TextWrapping$
 Wrap=�6h   $��50��6|   $	��Top=�6�   $��30��6�    J� 	AllowDrop .  6�   �� 5   
   -   5        $ ProbabilityLabel_Copy��5      $��%��6/   $
��Left=�6;   $��
120,77,0,0q�6V   $	��Top=�6j   $��20��6�   �� 5   
   -   5        $
��NEXT��5      $��331,342,0,0q�6    $	��Top=�65   $��46��6M   $��20(�6Y   $
��Bold"�6g   ���	#FF4DC929$
��Left=�6�   $��65��6�   �� 5   
   -   5         ��Name$ ChildrenEqual��5      $)��#All children have equal probability��6*   $
��Left=�6X   $��
10,115,0,0q�6s   $	��Top=�6�   $��15��6�   $	��205��6�   �� 5   
   -	   5         ��Name$ ExclusionDropDown��5      $
��Left=�6.   $��
10,215,0,0q�6I   $	��Top=�6]   $	��120��6u   �� 5   
   -
   5         ��Name$ AddExclusion��5      $��Button��6'   $
��Left=�68   $��135,215,0,0q�6S   $	��Top=�6h   $��75��6�   $��22��6�   6�   �� 5   
   -   5        $ ExclusionList��5      $
��Left=�6)   $	��132��6D   $��216,205,0,0q�6Q   $	��Top=�6f   $	��180��6~   �� 5   
   $
��Left=�6   $��450,195,-51,0q�6/   $
��Wrap=�6F   $��	TextBlock��6Z   $	��Top=�6k   �� 5   
   $
��Left=�6   $��10��6/   $��	6,170,0,0q�6;   $	��Top=�6N   $	��390��6f   �� 5   
   -   5        $ ExclusionsLabel��5      $��Layers to exclude��6)   $
��Left=�6E   $��
10,184,0,0q�6`   $	��Top=�6t   $	��120��6�   �� 5   
   $
��Left=�6   $��
10,242,0,0q�6/   $
��Wrap=�6C   $����These are layers you don't want to have along side with the current layer. This means if this layer is added, we will skip all of the layers mentioned in this box. These are all done in order, so make sure the layers here are not added BEFORE this layer.��6W   $	��Top=�6]  $	��100��6u  $	��200��6�  $��11(�6�  �� 5   
   $��
Exclusions��6   $
��Left=�6%   $��277,180,0,0q�6@   $	��Top=�6U   $��26��6m   $	��129��6y   5      5      