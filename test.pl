#!/usr/bin/perl

#  nota di link: ovviamente se il server non processa lo script, questa linea non è visibile.
$link = "Test Perl Riuscito";


print "Content-type: text/html\n\n";
print "<html><head><title>Server Status</title></head><body><br><BR>Il server sta usando la versione PERL $]\n<BR><BR>";
print $link;
print "</body></html>"


