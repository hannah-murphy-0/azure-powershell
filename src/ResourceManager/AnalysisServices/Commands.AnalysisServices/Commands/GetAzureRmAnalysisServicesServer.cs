﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.AnalysisServices.Models;
using Microsoft.Azure.Management.Analysis;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Management.Analysis.Models;

namespace Microsoft.Azure.Commands.AnalysisServices
{
    [Cmdlet(VerbsCommon.Get, "AzureRmAnalysisServicesServer", DefaultParameterSetName = BaseParameterSetName),
     OutputType(typeof(List<AnalysisServicesServer>))]
    [Alias("Get-AzureAs")]
    public class GetAzureAnalysisServicesServer : AnalysisServicesCmdletBase
    {
        internal const string BaseParameterSetName = "All In Subscription";
        internal const string ResourceGroupParameterSetName = "All In Resource Group";
        internal const string ServerParameterSetName = "Specific Server";

        [Parameter(ParameterSetName = ResourceGroupParameterSetName, Position = 0,
            ValueFromPipelineByPropertyName = true, Mandatory = true,
            HelpMessage = "Name of resource group under which want to retrieve the server.")]
        [Parameter(ParameterSetName = ServerParameterSetName, Position = 1, ValueFromPipelineByPropertyName = true,
            Mandatory = false, HelpMessage = "Name of resource group under which want to retrieve the server.")]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(ParameterSetName = ServerParameterSetName, Position = 0, ValueFromPipelineByPropertyName = true,
            Mandatory = true, HelpMessage = "Name of a specific server.")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        public override void ExecuteCmdlet()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                // Get for single server
                WriteObject(AnalysisServicesClient.GetServer(ResourceGroupName, Name));
            }
            else
            {
                // List all servers in given resource group if avaliable otherwise all servers in the subscription
                var list = AnalysisServicesClient.ListServers(ResourceGroupName);
                WriteObject(list, true);
            }
        }
    }
}