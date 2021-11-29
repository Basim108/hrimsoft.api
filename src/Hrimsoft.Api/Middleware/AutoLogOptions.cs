// Copyright © 2021 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the\nproperty of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual\nproperty law. Dissemination of this information or reproduction of this material is strictly forbidden,\nunless prior written permission is obtained from EPAM Systems, Inc

using Microsoft.Extensions.Logging;

namespace Hrimsoft.Api
{
    public class AutoLogOptions
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Debug;
    }
}