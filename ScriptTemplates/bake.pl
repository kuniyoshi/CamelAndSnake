#!/usr/bin/env perl -s
use 5.10.0;
use utf8;
use strict;
use warnings;
use open qw( :utf8 :std );
use Data::Dumper;
use Encode qw( encode decode );
use Readonly;
use Path::Class qw( dir file );
use Template;

Readonly my $Bom16Be => "\x{feff}";
Readonly my $NOTE => <<END_NOTE;
// *****************************************************
// *** This file will be baked from ScriptTemplates. ***
// *****************************************************
END_NOTE

our $dest;
die usage( ) unless defined $dest;
die "[$dest] does not exist" if !-d $dest;

my $tmpl = Template->new;

my $dir = dir( $dest );
bake( $_, $dir, $tmpl ) for @ARGV;

exit;

sub get_bom { # supports only u+feff
    my $file = file( shift );
    my $FH = $file->openr;
    read $FH, my $bom, 3;

    $bom = decode( "utf-8", $bom );
    undef $bom if $bom !~ m{\A $Bom16Be }msx;
    return unless $bom;
    return $bom;
}

sub bake {
    my( $filename, $dir, $tmpl ) = @_;

    die "No such file" if !-f $filename;
    die "Create file on the Unity Editor first (metafile cuase)."
        if !-f $dir->file( $filename );

#    my $bom = get_bom( $filename );
    my $input = file( $filename )->slurp( iomode => "<:encoding(UTF-8)" );

    $tmpl->process( \$input, { }, \my $output )
        or die $tmpl->error;

    $output =~ s{\n\n}{\n\n$NOTE\n}msx;
#    $output = $Bom16Be . $output if $bom;

    my $target = $dir->file( $filename );
    $target->spew( encode( "utf-8", $output ) );
}

sub usage {
    return <<END_USAGE;
usage: $0 -dest=<destination directory>
END_USAGE
}
