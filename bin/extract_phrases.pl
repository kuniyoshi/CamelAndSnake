#!/usr/bin/env perl
use 5.10.0;
use utf8;
use strict;
use warnings;
use open qw( :utf8 :std );
use Data::Dumper;
use Path::Class qw( file );

die usage( )
    unless @ARGV;

print_extracted( $_ )
    for @ARGV;

exit;

sub be_snake {
    my $keyword = shift;
    $keyword =~ s{[A-Z]}{_$&}g;
    $keyword =~ s{\A _ }{}msx;
    $keyword = lc $keyword;
    return $keyword;
}

sub has_several_words {
    my $phrase = shift;
    return $phrase =~ m{\A [A-Z]? [a-z]+ [A-Z] }msx;
}

sub extract_keywords {
    my $line = shift;
    my @candidates = $line =~ m{\b([A-Za-z][0-9A-Z_a-z]+)}gmsx;
    my @keywords = grep { has_several_words( $_ ) } @candidates;
    @keywords = map { be_snake( $_ ) } @keywords;
    return @keywords;
}

sub print_extracted {
    my $file = file( shift );
    my @lines = $file->slurp( chomp => 1 );
    my @keywords = map { extract_keywords( $_ ) } @lines;
    print map { $_, "\n" } @keywords;
}

sub usage {
    return <<END_USAGE;
usage: $0 <file name> [<file name>]
END_USAGE
}
