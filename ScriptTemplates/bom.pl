#!/usr/bin/perl
use 5.10.0;
use utf8;
use strict;
use warnings;
use open qw( :utf8 :std );
use Data::Dumper;
use Path::Class qw( file );
use Encode qw( decode );

my $file = file( "TheAnimatorId.cs" );

my $bom = get_bom( $file );
say Dumper $bom;

exit;

sub get_bom { # supports only u+feff
    my $file = shift;
    my $FH = $file->openr;
    read $FH, my $bom, 3;

    $bom = decode( "utf-8", $bom );
    undef $bom if $bom !~ m{\A \x{feff} }msx;
    return unless $bom;
    return $bom;
}

